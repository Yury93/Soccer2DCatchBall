using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public enum TaskPriorityEnum
    {
        Default,
        Forced,
    }


    
    public class TaskManager : MonoBehaviour
    {
        public static TaskManager manager;

        Coroutine currentCor = null;
        List<Task> tasks = new List<Task>();
        Task currentTask;


        private void Start()
        {
            manager = this;
        }
        /// <summary>
        /// Создает задачуи помещает в очередь задач (IEnumerator), НЕЛЬЗЯ вызывать из тех же корутин и колбэков(токль с задержкой в кадр)
        /// </summary>
        /// <param name="ienumerator"> IEnumerator(args)</param>
        /// <param name="onComplete">фунция вызывается когда задача завершена</param>
        /// <param name="onStop">функция вызывается если задачу остановила другая задача</param>
        /// <param name="priorityEnum">dafault - в конец очереди, high - в начало очереди , Forced - в начало очереди отменяя текущую, вызывая у нее onForcedStop, ее не может прервать обычный Forced, но может прервать, UltimateForced (использовать только при смерти оглушения и тд)</param>
        public void CreateTask(IEnumerator ienumerator, Action onComplete = null, Action onStop = null, TaskPriorityEnum priorityEnum = TaskPriorityEnum.Default)
        {
            Task task = new Task(ienumerator, onComplete, onStop, priorityEnum);
            onAdd(task);
        }
        void onAdd(Task newTask)
        {
            Debug.Log($"Task added id" + newTask.ienumerator + "  count " + tasks.Count);
            if (newTask.taskPriority == TaskPriorityEnum.Default)
            {
                tasks.Add(newTask);
                if (currentCor == null)
                {
                    startNextTask();
                }
            }
            else if (newTask.taskPriority == TaskPriorityEnum.Forced)
            {
                if (currentCor == null)
                {
                    tasks.Insert(0, newTask);
                    startNextTask();
                }
                else if (currentCor != null)
                {
                    StartCoroutine(forcedSwap(newTask));
                }
            }

        }
        IEnumerator forcedSwap(Task newTask)
        {
            
            StopCoroutine(currentCor);
            removeCurTaskFromList();
            currentCor = null;
            yield return new WaitForEndOfFrame();
            if (currentTask.onForcedStop != null) currentTask.onForcedStop.Invoke();
            yield return new WaitForEndOfFrame();
            tasks.Insert(0, newTask);
            startNextTask();
        }
        void startNextTask()
        {
            if (tasks.Count > 0)
            {
                startTask(tasks[0]);
            }
        }
        void startTask(Task task)
        {
            if (currentCor == null)
            {
                currentTask = task;
                StartCoroutine(startTaskCor(task));
            }
            else Debug.LogError("Пытаемся запустить новую задачу, но CurrentCor не обнулена");
        }
        IEnumerator startTaskCor(Task task)
        {
            //  Debug.Log("Task started ");
            yield return currentCor = StartCoroutine(task.ienumerator);
            //   Debug.Log("Task ended " );
            if (task.feedback != null) task.feedback.Invoke();
            currentCor = null;
            removeCurTaskFromList();
            startNextTask();
        }

        void removeCurTaskFromList()
        {
            tasks.Remove(currentTask);
        }





    }

    public class TaskQueue : MonoBehaviour
    {
        [SerializeField] bool enableDebug = true;
        Coroutine currentCor = null;
        List<Task> tasks = new List<Task>();
        Task currentTask;
        bool creatingForcedTaskProcess = false;
        /// <summary>
        /// создает задачу, перекрывающую старую
        /// </summary>
        /// <param name="ienumerator"></param>
        /// <param name="onComplete"></param>
        /// <param name="onStop"></param>
        protected void CreateForcedTask(IEnumerator ienumerator, Action onComplete = null, Action onStop = null)
        {
            Task task = new Task(ienumerator, onComplete, onStop, id: UnityEngine.Random.Range(1000, 9999));
            startFoceTask(task);
        }
        /// <summary>
        /// позволяет безопасно вызывать задачу из других задач.
        /// </summary>
        /// <param name="ienumerator"></param>
        /// <param name="onCOmplete"></param>
        /// <param name="onStop"></param>
        public void CreateForcedTaskWithDelay(IEnumerator ienumerator, Action onCOmplete = null, Action onStop = null)
        {

            Task task = new Task(ienumerator, onCOmplete, onStop, id: UnityEngine.Random.Range(1000, 9999));
            StartCoroutine(delay(task));
        }
        IEnumerator delay(Task task)
        {
            yield return new WaitForSeconds(0.03f);
            startFoceTask(task);
        }
        void startFoceTask(Task newTask)
        {
            if (!creatingForcedTaskProcess)
            {
                if (currentCor == null)
                {
                    currentTask = newTask;
                    StartCoroutine(forcedCorStarting(newTask));
                }
                else
                {
                    StartCoroutine(stopForcedTask(newTask));
                }
            }
            else Log($"<color=red>ОШИБКА, пытаемся запустить новую корутину{newTask.id}, но сейчас идет процесс запуска карутины </color>" + newTask.ienumerator);

        }
        IEnumerator stopForcedTask(Task newTask)
        {
            if (currentCor != null)
            {
                creatingForcedTaskProcess = true;
                StopCoroutine(currentCor);

                Log($"Силой остановили корутину{currentTask.id} от задачи " + currentTask.ienumerator + $" потому что пришла задача {newTask.id} {newTask.ienumerator}");
                if (currentTask.onForcedStop != null) currentTask.onForcedStop.Invoke();
                //   Log("Вызвали событие он форс " + currentTask.ienumerator);
                yield return new WaitForEndOfFrame();
                //     Log("После подождали кадр " + currentTask.ienumerator);
                currentCor = null;
                yield return new WaitForEndOfFrame();

                Log($"После ЕЩЕ подождали кадр , запускаем новую {newTask.id} корутину " + newTask.ienumerator);
                currentTask = newTask;
                StartCoroutine(forcedCorStarting(newTask));
                creatingForcedTaskProcess = false;
            }
            else Log("<color=red>ОШИБКА, пытаемся остановить силовую корутину а ее нету</color>");
        }
        IEnumerator forcedCorStarting(Task task)
        {
            Log($"Окончательно запускаем задачу{task.id} с корутиной " + task.ienumerator);
            yield return currentCor = StartCoroutine(task.ienumerator);
            Log($"Сама завершилась задача{task.id} с корутиной " + task.ienumerator);
            if (currentTask.feedback != null) currentTask.feedback.Invoke();
            currentTask = null;//НОВОЕ
            currentCor = null;

        }



        /// <summary>
        /// Создает задачу и помещает в очередь задач (IEnumerator). по окончанию вызывается OnCOmplete
        /// </summary>
        /// <param name="ienumerator"> IEnumerator(args)</param>
        /// <param name="callback">фунция вызывается когда задача завершена</param>
        /// <param name="onComplete">функция вызывается если задачу остановила другая задача</param>
        /// <param name="priorityEnum">dafault - в конец очереди, high - в начало очереди , Forced - в начало очереди отменяя текущую, вызывая у нее onForcedStop</param>
        protected void CreateQueueTask(IEnumerator ienumerator,  Action onComplete = null)
        {
            Task task = new Task(ienumerator, onComplete );
            onAdd(task);
        }

        void onAdd(Task newTask)
        {
            string last = "";
            if (tasks.Count > 0)
            {
                last += tasks[0].ienumerator;
            }
            else last += "нету";

            Log(gameObject.name + " Добавил задачу " + newTask.ienumerator + "  до этого было задач " + tasks.Count + "Предыдущая карутина = " + last);

            tasks.Add(newTask);
            if (currentCor == null)
            {
                startNextTask();
            }
           
        }

        IEnumerator forcedStopCurrentTask()
        {
            tasks.Clear(); // removeCurTask();
                           //  Log("Силой ОСТАНАВЛИВАЕМ корутину " + currentTask.ienumerator);
            StopCoroutine(currentCor);
            // Log("Силой остановили корутину " + currentTask.ienumerator);
            currentCor = null;
            if (currentTask.onForcedStop != null) currentTask.onForcedStop.Invoke();


            yield return new WaitForEndOfFrame();

            Log("Силой остановили корутину И подождали секунду щас запустим новую " + currentTask.ienumerator);
            startNextTask();
        }
        void startNextTask()
        {

            if (tasks.Count > 0)
            {
                Log("так как лист задачне НЕ пуст запускаем = " + tasks[0].ienumerator);
                startTask(tasks[0]);

            }
            else
                Log("пытались запустить следующую задау но их нет ");
        }
        void startTask(Task task)
        {
            if (currentCor == null)
            {
                currentTask = task;
                StartCoroutine(startTaskCor(task));
            }
            else Log("<color=red> Пытаемся запустить новую задачу, но CurrentCor не обнулена</color>");
        }
        IEnumerator startTaskCor(Task task)
        {
            Log("ОКОНЧАТЕЛЬНО Запустили задачу " + task.ienumerator + " с приоритетом " + task.taskPriority);
            yield return currentCor = StartCoroutine(task.ienumerator);
            Log("Задача " + task.ienumerator + " завершена");
            if (task.feedback != null) task.feedback.Invoke();
            currentCor = null;
            removeCurTask();

            yield return new WaitForEndOfFrame();
            startNextTask();
        }


        void removeCurTask()
        {
            tasks.Remove(currentTask);
        }




        protected void StopAllTasks()
        {
            Debug.Log("StopAllTasks() from " + gameObject.name);
            if (currentCor != null) StopCoroutine(currentCor);


            if (currentTask.onForcedStop != null) currentTask.onForcedStop.Invoke();
            tasks.Clear();

            if (currentCor != null) StopCoroutine(currentCor);
            currentCor = null;
        }
     
        protected void StopCurrentTask()
        {
            if (currentCor != null)
            {

                StopCoroutine(currentCor);
                removeCurTask();
            }
            Debug.Log("Силой остановили ТЕКУЩУЮ корутину " + currentTask.ienumerator);
            currentCor = null;
            if (currentTask.onForcedStop != null) currentTask.onForcedStop.Invoke();
        }


        protected void Log(string m)
        {
            if (enableDebug) Debug.Log(m);

        }
    }


    public class Task
    {
        public int id;
        public Task(IEnumerator ienumerator, Action feedback = null, Action onForcedStop = null, TaskPriorityEnum taskPriority = TaskPriorityEnum.Default, int id = 0)
        {
            this.id = id;
            this.ienumerator = ienumerator;
            this.feedback = feedback;
            this.onForcedStop = onForcedStop;
            this.taskPriority = taskPriority;
        }
        public TaskPriorityEnum taskPriority = TaskPriorityEnum.Default;

        public Action feedback;
        public Action onForcedStop;

        public IEnumerator ienumerator;



    }
}
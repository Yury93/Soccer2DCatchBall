using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent onCollision,onCollisionExit;
    public string sendMessage;
    [Tooltip("Таги которые будут проходить проверку")]
    public List<string> tags;
    public Helpers.Clip clip;
    [Tooltip("Создает ГО при коллизии")]
    public GameObject onGoodTagEffect, onDestroyTagEffect;
    public enum CreateCondition { myPosition, targetPosition };
    public CreateCondition positionOfEffect;
    [Tooltip("После каких коллизий будет уничтожение")]
    public List<string> destroyTags;
    [SerializeField] bool destroyOnGoodTagCollision;
    void Start()
    {

    }

    void onGoodCollision(GameObject target)
    {
        if (onGoodTagEffect != null)
        {
            if (positionOfEffect == CreateCondition.myPosition) Instantiate(onGoodTagEffect, transform.position, Quaternion.identity);
            if (positionOfEffect == CreateCondition.targetPosition) Instantiate(onGoodTagEffect, target.transform.position, Quaternion.identity);
        }
        if (clip.clip != null)
        {
            Helpers.SoundCreator.creator.CreateSound(clip.clip, Helpers.Sound.SoundType.ui, volume: clip.volume);
        }
        onCollision.Invoke();
        if (destroyOnGoodTagCollision) Destroy(gameObject);
    }
    private void onDestroyCollision()
    {
        if (onDestroyTagEffect != null)
        {
          Instantiate(onDestroyTagEffect, transform.position, Quaternion.identity);
        }
        if (clip.clip != null)
        {
            Helpers.SoundCreator.creator.CreateSound(clip.clip, Helpers.Sound.SoundType.ui, volume: clip.volume);
        }
        Destroy(gameObject);
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tags.Contains(collision.gameObject.tag))
        {
            if (!string.IsNullOrEmpty(sendMessage)) collision.gameObject.SendMessage(sendMessage);
            onGoodCollision(collision.gameObject);
        }
        if (destroyTags.Contains(collision.gameObject.tag)) onDestroyCollision();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tags.Contains(collision.gameObject.tag))
        {
            if (!string.IsNullOrEmpty(sendMessage)) collision.gameObject.SendMessage(sendMessage);
            onGoodCollision(collision.gameObject);
        }
        if (destroyTags.Contains(collision.gameObject.tag)) { onDestroyCollision(); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tags.Contains(collision.gameObject.tag))
        {
            onCollisionExit.Invoke();
        }
    }
}

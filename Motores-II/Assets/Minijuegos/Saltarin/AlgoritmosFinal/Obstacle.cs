using UnityEngine;

public abstract class Obstacle : MonoBehaviour, IObstacle
{

    public Transform Follow;

    protected MeshRenderer meshRenderer;
    protected Collider _collider;

    protected virtual void Start()
    {
        _collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();

        TurnOff();
    }
    protected virtual void Update()
    {
        if (Follow != null)
        {
            FollowLider();
        }
    }

    protected virtual void FollowLider(float altura = 1)
    {
        transform.position = new Vector3(Follow.position.x, altura, Follow.position.z);
    }

    public virtual void TurnOn()
    {
        meshRenderer.enabled = true;
        _collider.enabled = true;
    }

    public virtual void TurnOff()
    {
        meshRenderer.enabled = false;
        _collider.enabled = false;
    }

    public virtual void Touch()
    {

    }

}

public interface IObstacle
{
    public virtual void TurnOn()
    {

    }

    public virtual void TurnOff()
    {

    }

    public virtual void Touch()
    {

    }
}

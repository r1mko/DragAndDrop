using UnityEngine;

public class PlayerReach : MonoBehaviour
{
    public float reachDistance = 5f;
    public Selectable currentSelectable;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            Debug.Log("Object hit: " + hit.collider.gameObject.name);

            //Highlight with pickable objects
            Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>();
            if (selectable)
            {
                if (currentSelectable && currentSelectable != selectable)
                {
                    currentSelectable.Deselect();
                }
                currentSelectable = selectable;
                selectable.Select();
            }
                else
                {
                    if (currentSelectable)
                    {
                        currentSelectable.Deselect();
                        currentSelectable = null;
                    }
                }
        }
        else
        {
            if (currentSelectable)
            {
                currentSelectable.Deselect();
                currentSelectable = null;
            }
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * reachDistance, Color.green);
        }
    }

    public bool IsRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit, reachDistance);
    }
}
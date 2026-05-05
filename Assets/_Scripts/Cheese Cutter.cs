using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

public class CheeseCutter : MonoBehaviour
{
    // Events for UI or other systems to subscribe to
    public FloatEvent OnEstimatedSliceWeight = new FloatEvent();
    public GameObjectEvent OnSliceCreated = new GameObjectEvent();

    // Simple tuning parameters
    public float sensitivity = 1f; // how fast mouse/keys change the cut amount
    public float cheeseDensity = 1f; // units of weight per unit volume (arbitrary)
    public Transform cutMarker; // optional visual marker to show cut position

    // Internal state
    private GameObject activeCheese;
    private float requestedWeight;
    private bool cutting = false;
    private float cutFraction = 0.5f; // 0..1 fraction of the cheese to slice off

    // Begin a cutting session for a selected cheese and the customer's requested weight
    public void BeginCut(GameObject cheese, float requestedWeight)
    {
        if (cheese == null) return;
        activeCheese = cheese;
        this.requestedWeight = requestedWeight;
        cutting = true;
        cutFraction = 0.5f;
        UpdateCutMarker();
    }

    // Cancel the current cut
    public void CancelCut()
    {
        cutting = false;
        activeCheese = null;
        UpdateCutMarker();
    }

    // Commit the cut and create a placeholder slice object. Replace this with a proper mesh-slicing
    // implementation when available.
    public void CommitCut()
    {
        if (!cutting || activeCheese == null) return;

        float estimatedWeight = EstimateSliceWeight(activeCheese, cutFraction);

        // Create a simple placeholder slice object (cube) sized proportionally to the cut fraction.
        // This is an approximation for prototyping; replace with real mesh slicing later.
        Renderer r = activeCheese.GetComponent<Renderer>();
        Vector3 size = Vector3.one;
        Vector3 min = activeCheese.transform.position;
        if (r != null)
        {
            size = r.bounds.size;
            min = r.bounds.min;
        }

        GameObject slice = GameObject.CreatePrimitive(PrimitiveType.Cube);
        slice.name = activeCheese.name + "_Slice";
        // Scale so the slice represents cutFraction along the X axis (approximation)
        slice.transform.localScale = new Vector3(size.x * cutFraction, size.y, size.z);

        // Position slice at the 'start' side of the cheese (using world bounds approximation)
        Vector3 sliceCenter = min + new Vector3(size.x * cutFraction / 2f, size.y / 2f, size.z / 2f);
        slice.transform.position = sliceCenter;

        // Add a rigidbody so it can be interacted with
        Rigidbody rb = slice.AddComponent<Rigidbody>();
        rb.mass = Mathf.Max(0.0001f, estimatedWeight);

        // Notify listeners
        OnEstimatedSliceWeight.Invoke(estimatedWeight);
        OnSliceCreated.Invoke(slice);

        // End cutting session
        cutting = false;
        activeCheese = null;
        UpdateCutMarker();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutting || activeCheese == null) return;

        // Input: mouse horizontal movement and arrow keys change the cut fraction
        float delta = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) delta += sensitivity * 0.5f * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) delta -= sensitivity * 0.5f * Time.deltaTime;

        cutFraction = Mathf.Clamp01(cutFraction + delta);

        float estimatedWeight = EstimateSliceWeight(activeCheese, cutFraction);
        OnEstimatedSliceWeight.Invoke(estimatedWeight);

        UpdateCutMarker();

        // Commit with space or left mouse button, cancel with escape or right mouse button
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            CommitCut();
        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            CancelCut();
        }
    }

    // Estimate slice weight by approximating cheese volume from renderer bounds and applying density
    private float EstimateSliceWeight(GameObject cheese, float fraction)
    {
        if (cheese == null) return 0f;
        Renderer r = cheese.GetComponent<Renderer>();
        Vector3 size = Vector3.one;
        if (r != null) size = r.bounds.size;
        float volume = Mathf.Abs(size.x * size.y * size.z);
        float sliceVolume = Mathf.Clamp01(fraction) * volume;
        return sliceVolume * cheeseDensity;
    }

    private void UpdateCutMarker()
    {
        if (cutMarker == null || activeCheese == null)
        {
            if (cutMarker != null) cutMarker.gameObject.SetActive(false);
            return;
        }

        Renderer r = activeCheese.GetComponent<Renderer>();
        if (r == null)
        {
            cutMarker.gameObject.SetActive(false);
            return;
        }

        cutMarker.gameObject.SetActive(true);
        Vector3 min = r.bounds.min;
        Vector3 size = r.bounds.size;
        Vector3 worldPos = min + new Vector3(size.x * cutFraction, size.y * 0.5f, size.z * 0.5f);
        cutMarker.position = worldPos;
    }
}

using UnityEngine;
using Riptide;

public class MessageSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deviceAccel = Input.acceleration;

        deviceAccel = Quaternion.Euler(90, 0, 0) * deviceAccel;

        NetworkManager.Instance.SendMessageToServer(deviceAccel);
    }

    private void OnEnable()
    {
        Debug.Log("MessageSender activado");
    }
}

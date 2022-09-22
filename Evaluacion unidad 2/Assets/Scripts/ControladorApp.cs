using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.UI;

enum TaskState
{
    INIT,
    WAIT_COMMANDS
}

public class ControladorApp : MonoBehaviour
{
    private static TaskState taskState = TaskState.INIT;
    private SerialPort _serialPort;
    private byte[] buffer;
    public TextMeshProUGUI leds;
    public TextMeshProUGUI estadoLeds;
    public TextMeshProUGUI estadoBtn1;
    public TextMeshProUGUI estadoBtn2;
    public TextMeshProUGUI estadoBtn3;
    public Button btnLed;
    public Button btnReadBtn;
    public int led=0;

    public void btnledcont()
    {
        if (led == 0)
        {
            _serialPort.Write("led1\n");
            Debug.Log("Send led1");
            leds.text = "1";
            estadoLeds.text = "ON";
            led = 1;
        }
        else if (led == 1)
        {
            _serialPort.Write("led2\n");
            Debug.Log("Send led2");
            leds.text = "2";
            estadoLeds.text = "ON";
            led = 2;
        }
        else if (led == 2)
        {
            _serialPort.Write("led3\n");
            Debug.Log("Send led3");
            leds.text = "3";
            estadoLeds.text = "ON";
            led = 3;
        }
        else if (led == 3)
        {
            _serialPort.Write("led1\n");
            Debug.Log("Send led1");
            leds.text = "1";
            estadoLeds.text = "ON";
            led = 1;
        }
    }

    public void btnBtRead()
    {
        _serialPort.Write("readBUTTONS\n");
        Debug.Log("Send readBUTTONS");
    }
    void Start()
    {
        _serialPort = new SerialPort();
        _serialPort.PortName = "/dev/ttyUSB0";
        _serialPort.BaudRate = 115200;
        _serialPort.DtrEnable = true;
        _serialPort.NewLine = "\n";
        _serialPort.Open();
        Debug.Log("Open Serial Port");
        buffer = new byte[128];
    }

    void Update()
    {
        switch (taskState)
        {
            case TaskState.INIT:
                taskState = TaskState.WAIT_COMMANDS;
                Debug.Log("WAIT COMMANDS");
                break;
            case TaskState.WAIT_COMMANDS:
                
                if (btnLed == null)
                {
                    btnledcont();
                }
                
                if (btnReadBtn == null)
                {
                    btnBtRead();
                }
                if (_serialPort.BytesToRead > 0)
                {
                    string response = _serialPort.ReadLine();
                    if (response=="btn1: OFF"||response=="btn1: ON")
                    {
                        estadoBtn1.text=response;
                    }
                    if (response=="btn2: OFF"||response=="btn2: ON")
                    {
                        estadoBtn2.text=response;
                    }
                    if (response=="btn3: OFF"||response=="btn3: ON")
                    {
                        estadoBtn3.text=response;
                    }
                    Debug.Log(response);
                }

                break;
            default:
                Debug.Log("State Error");
                break;
        }
    }
}

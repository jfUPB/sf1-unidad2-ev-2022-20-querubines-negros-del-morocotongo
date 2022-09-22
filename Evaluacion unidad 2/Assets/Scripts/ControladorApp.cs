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
    
    public void btnState(){
        string response = _serialPort.ReadLine();
        if (response == "btn1: ON btn2: ON btn3: ON")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "btn1: ON btn2: ON btn3: OFF")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Released";
        }
        if (response == "btn1: ON btn2: OFF btn3: ON")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "btn1: OFF btn2: ON btn3: ON")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "btn1: OFF btn2: OFF btn3: ON")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Pressed";
        }
        if (response == "btn1: ON btn2: OFF btn3: OFF")
        {
            estadoBtn1.text = "1: Pressed";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Released";
        }
        if (response == "btn1: OFF btn2: ON btn3: OFF")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Pressed";
            estadoBtn3.text = "3: Released";
        }
        if (response == "btn1: OFF btn2: OFF btn3: OFF")
        {
            estadoBtn1.text = "1: Released";
            estadoBtn2.text = "2: Released";
            estadoBtn3.text = "3: Released";
        }
        Debug.Log(response);
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
                    btnState();
                }

                break;
            default:
                Debug.Log("State Error");
                break;
        }
    }
}

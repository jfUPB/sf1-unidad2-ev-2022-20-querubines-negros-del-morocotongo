#include <Arduino.h>
#include "task1.h"
#include "events.h"

String btnState(uint8_t btnState)
{
    if (btnState == HIGH)
    {
        return "OFF";
    }
    else
        return "ON";
}

void task1()
{
    enum class TaskState
    {
        INIT,
        WAIT_COMMANDS,
        SEND_EVENT
    };
    static TaskState taskState = TaskState::INIT;
    constexpr uint8_t led1 = 14;
    constexpr uint8_t led2 = 25;
    constexpr uint8_t led3 = 26;
    constexpr uint8_t button1Pin = 13;
    constexpr uint8_t button2Pin = 12;
    constexpr uint8_t button3Pin = 33;

    switch (taskState)
    {
    case TaskState::INIT:
    {
        Serial.begin(115200);
        pinMode(led1, OUTPUT);
        pinMode(led2, OUTPUT);
        pinMode(led3, OUTPUT);
        pinMode(button1Pin, INPUT_PULLUP);
        pinMode(button2Pin, INPUT_PULLUP);
        pinMode(button3Pin, INPUT_PULLUP);
        taskState = TaskState::WAIT_COMMANDS;
        break;
    }
    case TaskState::WAIT_COMMANDS:
    {
        if (Serial.available() > 0)
        {
            String command = Serial.readStringUntil('\n');
            if (command == "led1")
            {
                digitalWrite(led1, HIGH);
                digitalWrite(led2, LOW);
                digitalWrite(led3, LOW);
            }
            else if (command == "led2")
            {
                digitalWrite(led1, LOW);
                digitalWrite(led2, HIGH);
                digitalWrite(led3, LOW);
            }
            else if (command == "led3")
            {
                digitalWrite(led1, LOW);
                digitalWrite(led2, LOW);
                digitalWrite(led3, HIGH);
            }
            else if (command == "readBUTTONS")
            {

                Serial.print("btn1: ");
                Serial.print(btnState(digitalRead(button1Pin)).c_str());
                Serial.print("\n");
                Serial.print(" btn2: ");
                Serial.print(btnState(digitalRead(button2Pin)).c_str());
                Serial.print("\n");
                Serial.print(" btn3: ");
                Serial.print(btnState(digitalRead(button3Pin)).c_str());
                Serial.print('\n');
            }
        }
    }
    default:
    {
        break;
    }
    }
}

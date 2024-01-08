#include <Servo.h>

Servo myservo;  // create servo object to control a servo

const int smokePin = A5;
const int buzzerPin = 8;
const int flamePin = 9;
int isFlame = HIGH;
const int relayPin = 10;
const int pirPin = 11;
const int redLed = 12;
const int redLed1 = 7;
const int greenLed = 6;
const int greenLed1 = 5;
int sensorThres = 500;

int state = LOW;             // by default, no motion detected
int val = 0;
String password = "cal";
bool isPasswordCorrect = false;

void setup() {
  Serial.begin(9600);
  myservo.attach(4);
  pinMode(relayPin, OUTPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(flamePin, INPUT);
  pinMode(smokePin, INPUT);
  pinMode(redLed, OUTPUT);
  pinMode(redLed1, OUTPUT);
  pinMode(greenLed, OUTPUT);
  pinMode(greenLed1, OUTPUT);
  digitalWrite(relayPin, LOW); // Ensure the relay is initially off
}

void loop() {
  if (Serial.available() > 0) {
    String receivedPassword = Serial.readStringUntil('\n');
    receivedPassword.trim(); // Remove leading and trailing whitespaces

    if (receivedPassword.equals(password)) {
      isPasswordCorrect = true;
      digitalWrite(relayPin, HIGH); // Activate the relay
      Serial.println("Correct Password");

      // Move the servo motor to 180 degrees
      myservo.write(180);
      delay(3000); // Add a delay for the servo to reach the desired position
      myservo.write(0);
      delay(5000); // Add a delay for the servo to reach the desired position

      
    } else {
      isPasswordCorrect = false;
      digitalWrite(relayPin, LOW); // Deactivate the relay
      Serial.println("Incorrect Password");

      // Move the servo motor to 0 degrees
      myservo.write(0);
      delay(3000); // Add a delay for the servo to reach the desired position
    }
  }

  isFlame = digitalRead(flamePin);
  int smokeValue = analogRead(smokePin);
  Serial.print("Flame Sensor Value: ");
  Serial.println(isFlame);
  Serial.print("Smoke Sensor Value: ");
  Serial.println(smokeValue);
 
  // Check if either sensor detects a potential threat
  if (isFlame == LOW || smokeValue > sensorThres) {
    digitalWrite(buzzerPin, HIGH);
    Serial.println("Fire/Smoke Detected!");
    delay(1000);
  } else {
    digitalWrite(buzzerPin, LOW);
  }

  delay(1000);  // Delay for smoother operation

  val = digitalRead(pirPin); // Read the value from the PIR sensor

  if (val == HIGH) {           // check if the sensor is HIGH
    digitalWrite(redLed, HIGH);
    digitalWrite(redLed1, HIGH);
    digitalWrite(greenLed, HIGH);
    digitalWrite(greenLed1, HIGH);   // turn LED ON
    delay(100);                // delay 100 milliseconds 
    
    if (state == LOW) {
      Serial.println("Motion detected!"); 
      state = HIGH;       // update variable state to HIGH
    }
  } else {
    digitalWrite(redLed, LOW);
    digitalWrite(redLed1, LOW);
    digitalWrite(greenLed, LOW);
    digitalWrite(greenLed1, LOW); // turn LED OFF
    delay(200);             // delay 200 milliseconds
      
    if (state == HIGH) {
      Serial.println("Motion stopped!");
      state = LOW;       // update variable state to LOW
    }
  }
}

#include <Servo.h>
uint16_t lightValue1, lightValue2, servoAngle;

Servo myServo; // Create a servo object

int sensor1 = A0; 
int sensor2 = A1; 

void setup() {
  myServo.attach(9);  
  Serial.begin(9600); 

  
}

void loop() {
  // Read sensor values
  int lightValue1 = analogRead(sensor1);
  int lightValue2 = analogRead(sensor2);

  // Calculate the servo angle based on the difference in sensor values
  int servoAngle = map(lightValue1 - lightValue2, -512, 512, 0, 180);
  servoAngle = constrain(servoAngle, 0, 180);

  // Move the servo motor to the calculated angle
  myServo.write(servoAngle);

  // Send sensor values to the serial port for communication with the C# application
  Serial.print((String)lightValue1 + "A" + lightValue2 + "B" + servoAngle + "C" + "\n");

  
  delay(300);

  
}

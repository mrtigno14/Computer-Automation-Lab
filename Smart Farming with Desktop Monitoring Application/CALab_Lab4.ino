int green = 10;
int yellow = 11;
int red = 13;

int relay_pin = 8;
int sensor_pin = A0; 

int buttonPin = A1; 
int currentState = 0;
int prevState = 0;
bool buttonPressed = false;

uint16_t output_value;

void setup()         
{ 
  pinMode(buttonPin, INPUT);           
  pinMode(relay_pin, OUTPUT);
  pinMode(sensor_pin, INPUT);
  Serial.begin(9600);  


}

void loop()
{
 int output_value = analogRead(sensor_pin);
 output_value = map(output_value, 550, 10, 0, 100);

 if(output_value <= 40){
  digitalWrite(relay_pin, LOW);
  digitalWrite(red, LOW);
  digitalWrite(green, HIGH);
  digitalWrite(yellow, LOW);
  }
  
  else if(output_value > 40 && output_value <= 80){
  digitalWrite(relay_pin, HIGH);
  digitalWrite(green, LOW);
  digitalWrite(red, LOW);
  digitalWrite(yellow, HIGH);       
 }
 else if(output_value > 81 && output_value <= 101){
  digitalWrite(relay_pin, HIGH);
  digitalWrite(green, LOW);
  digitalWrite(red, HIGH);
  digitalWrite(yellow, LOW);      
 }
 
 char input = Serial.read();
 
 if (input == '1'){ 
 digitalWrite(relay_pin, HIGH); 
 }
 if (input == '0'){
  digitalWrite(relay_pin, LOW); 
  }
  
  Serial.print((String)output_value + "A" + "\n");
  delay(300);

  buttonClick();
  

}

void buttonClick()
{
  int buttonState = digitalRead(buttonPin);
  if (buttonState != prevState) {
    delay(50);
    buttonState = digitalRead(buttonPin);
    }
    if (buttonState == LOW && !buttonPressed) {
    currentState = 0; 
    digitalWrite(relay_pin, HIGH);  
    buttonPressed = true;
    delay(200);
    }

    else if (buttonState == LOW && buttonPressed) {
      currentState = (currentState + 1) % 3;
      delay(200);
      }
      else if (buttonState == HIGH && buttonPressed) {
        buttonPressed = false;
         digitalWrite(relay_pin, LOW);  
        }
        prevState = buttonState;


}
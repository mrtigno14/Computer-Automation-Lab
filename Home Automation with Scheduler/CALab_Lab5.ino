#include <Keypad.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

const int relayPin = 10;

int enteredPassword[4] = {0, 0, 0, 0}; // Inputted values
const int correctPassword[4] = {1, 2, 3, 4}; // Correct password
int var = 0;

const byte rows = 4;
const byte columns = 4;
char keyMap[rows][columns] = {
  {'1', '2', '3', 'A'},
  {'4', '5', '6', 'B'},
  {'7', '8', '9', 'C'},
  {'*', '0', '#', 'D'}
};

byte rowPins[rows] = {9, 8, 7, 6};
byte colPins[columns] = {5, 4, 3, 2};
Keypad keypad = Keypad(makeKeymap(keyMap), rowPins, colPins, rows, columns);

LiquidCrystal_I2C lcd(0x27, 16, 2);

void setup() {
  lcd.begin(16, 2);
  lcd.init();
  lcd.backlight();
  Serial.begin(9600);
  pinMode(relayPin, OUTPUT);
}

void loop() {
  char key = keypad.getKey();

  if (key) {
    lcd.setCursor(6 + var, 1);
    lcd.print("*");
    lcd.setCursor(6 + var, 1);
    key -= 48;
    enteredPassword[var] = key;
    var++;

    if (var == 4) {
      delay(100);

      if (checkPassword()) {
        handlePasswordCorrect();
      } else {
        handlePasswordIncorrect();
      }

      var = 0;
      lcd.clear();
    }
  }

  // Your other code for serial input
  if (Serial.available() > 0) {
    char input = Serial.read();
    if (input == '1') {
      // Perform the activation logic when the password is correct
      digitalWrite(relayPin, HIGH);
    }
    if (input == '2') {
      digitalWrite(relayPin, LOW);
    }
  }

  if(!key){lcd.setCursor(0,0),lcd.print("Enter Password: ");}

  delay(2);
}

bool checkPassword() {
  for (int i = 0; i < 4; i++) {
    if (enteredPassword[i] != correctPassword[i]) {
      return false;
    }
  }
  return true;
}

void handlePasswordCorrect() {
  digitalWrite(relayPin, HIGH);
  lcd.clear();
  lcd.setCursor(4,0);
  lcd.print("Correct");
  lcd.setCursor(4,1);
  lcd.print("Password");
  delay(1000); 
  lcd.clear();
  Serial.println("Correct Password");
}
void handlePasswordIncorrect() {
  digitalWrite(relayPin, LOW);
  lcd.clear();
  lcd.setCursor(4,0);
  lcd.print("Incorrect");
  lcd.setCursor(4,1);
  lcd.print("Password");
  delay(1000); 
  lcd.clear();
  Serial.println("Incorrect Password");
}

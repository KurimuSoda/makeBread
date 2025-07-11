#include <M5Unified.h>
#include <BluetoothSerial.h>
#include <MFRC522_I2C.h>
#include "M5StickCPlus2.h"


#define UPPER_LIMIT 230.0 //ジャイロXの上判定ライン
#define LOWER_LIMIT -230.0 //ジャイロXの下判定ライン
#define UPPERAcc_LIMIT 1.0
#define LOWERAcc_LIMIT -1.0

M5Canvas canvas(&M5.Lcd);

float gyroX = 0.0f;
float gyroY = 0.0f;
float gyroZ = 0.0f;
float accX = 0.0f;
float accY = 0.0f;
float accZ = 0.0f;
int upCount = 0;
bool isShaked = false;
bool isBtnAPls = false;
bool isReadRFID = false;

String rfidstr = "";

const uint8_t buttonA_GPIO = 37;
uint8_t batteryLevel;

BluetoothSerial BTSerial;
MFRC522_I2C mfrc522_i2c(0x28, 2);
static String data = "";

void read_rfid(){
  String s = "";
  
  if (mfrc522_i2c.PICC_IsNewCardPresent() && mfrc522_i2c.PICC_ReadCardSerial()) {
    for (size_t i = 0; i < mfrc522_i2c.uid.size; ++i) {
      const auto d = mfrc522_i2c.uid.uidByte[i];
      s += String(d < 0x10 ? "0" : "") + String(d, HEX);
      
    }
    //BTSerial.println(s);
    s.toUpperCase();
  }
  if (s == "") {
    ::delay(200);
    return;
  }
  data = s;
  M5.Lcd.println(data);
  BTSerial.println(data);
  Serial.println(data);
  delay(200);
  BTSerial.println("");
  Serial.println("");

}

void read_serial(){
  rfidstr = "";
  if(BTSerial.available()){ //Bluetoothデータ受信で
    String msg = BTSerial.readStringUntil('\r'); //BTシリアルで送られてきた文字列をmsgに格納
    M5.Lcd.println("Received: " + msg); //M5Stickの画面に文字列msgを表示
    //BTSerial.print(msg);  //BTシリアルに受け取ったmsgを送信
    
    for(int i = 0; i < 3; i++){
      //BTSerial.println(msg[i]);
      rfidstr += msg[i];
      //BTSerial.println(rfidstr);
    }
    if(rfidstr == "rOn"){
      isReadRFID = true;
      M5.Lcd.println("ReaderOn!");
      rfidstr = "";
    }
    else if(rfidstr == "rOf"){
      isReadRFID = false;
      M5.Lcd.println("ReaderOff!");
      rfidstr = "";
    }
  }
  else if(Serial.available()){
    String msg = Serial.readStringUntil('\r');
    for(int i = 0; i < 3; i++){
      //Serial.println(msg[i]);
      rfidstr += msg[i];
      //Serial.println(rfidstr);
    }
    if(rfidstr == "rOn"){
      isReadRFID = true;
      M5.Lcd.println("ReaderOn!");
      rfidstr = "";
    }
    else if(rfidstr == "rOf"){
      isReadRFID = false;
      M5.Lcd.println("ReaderOff!");
      rfidstr = "";
    }
  }
}

void getShaked(){
  auto imu_update = M5.Imu.update();
  if(imu_update){
    M5.Imu.getGyro(&gyroX, &gyroY, &gyroZ);
    M5.Imu.getAccel(&accX, &accY, &accZ);
    //Serial.printf("accX:%0.2f, accY:%0.2f, acc:%0.2f\n", accX, accY, accZ);

    /*if(gyroX >= UPPER_LIMIT){
      upCount++;
    }*/
    if(accX >= UPPERAcc_LIMIT || gyroX >= UPPER_LIMIT){
      upCount++;
    }

    /*if(gyroX <= LOWER_LIMIT && upCount > 0){
      upCount = 0;
      isShaked = true;
    }*/
    if(accX <= LOWERAcc_LIMIT || gyroX <= LOWER_LIMIT && upCount > 0){
      upCount = 0;
      isShaked = true;
    }

    if(isShaked){
      BTSerial.println("Shaked");
      Serial.println("Shaked");
      delay(50);
      BTSerial.println("");
      Serial.println("");
      isShaked = false;
    }
    else { return; }
  }
}

void getButton(){
  if (M5.BtnA.wasPressed()) {
    //Serial.println("A Btn Released");
    Serial.println("ButtonA");
    BTSerial.println("ButtonA");
    M5.Lcd.println("Button A"); 
    delay(20);
  }
}

void spriteCanvas(){
  canvas.fillScreen(BLACK);
  canvas.setCursor(0,0,2);
  canvas.println("BTSerial, RFID! XD");
  canvas.printf("BAT Level : %3d%%\n", M5.Power.getBatteryLevel());

  canvas.pushSprite(&M5.Lcd,0,0);
}

static void buttonA_isr(void) {
  M5.update();
  //Serial.printf("A interrupt %d\n", millis() );
  BTSerial.println("ButtonA");
  Serial.println("ButtonA");
  isBtnAPls = true;

}

void setup() {
  // put your setup code here, to run once:
  M5.begin();
  M5.Imu.init();
  M5.Power.begin();
  M5.Lcd.setTextFont(1);
  M5.Lcd.setCursor(0, 0, 2);
  M5.Lcd.println("BTSerial, RFID! :D");
  BTSerial.begin("M5StickCPlus2BT");
  Serial.begin(115200);
  M5.Ex_I2C.begin();
  mfrc522_i2c.PCD_Init();

  attachInterrupt(digitalPinToInterrupt(buttonA_GPIO), buttonA_isr, FALLING);
  M5.Lcd.printf("BAT Level : %3d %%\n", M5.Power.getBatteryLevel());

  canvas.createSprite(M5.Lcd.width(), M5.Lcd.height());
  
}

void loop() {
  // put your main code here, to run repeatedly:
  M5.update();
  //getButton();
  //Serial.println("first");
  
  if(M5.BtnPWR.wasPressed()){
    Serial.println("Pow Pressed");
    //M5.Lcd.printf("BAT Level : %3d%%\n", M5.Power.getBatteryLevel());
    //spriteCanvas();
  }
  
  if(isBtnAPls == true){
    Serial.println("Btn A by loop");
    M5.Lcd.setCursor(0,32);
    spriteCanvas();
    isBtnAPls = false;
  }
  read_serial();
  if(isReadRFID == true){
    read_rfid();
  }
  else{}
  
  getShaked();

  //Serial.println("loop");
  
  //M5.Lcd.printf("BAT Level : %3d%%\n", M5.Power.getBatteryLevel());
}

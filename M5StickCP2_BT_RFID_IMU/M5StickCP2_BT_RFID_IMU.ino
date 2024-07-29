#include <M5Unified.h>
#include <BluetoothSerial.h>
#include <MFRC522_I2C.h>
#include "M5StickCPlus2.h"


#define UPPER_LIMIT 270.0 //ジャイロXの上判定ライン
#define LOWER_LIMIT -250.0 //ジャイロXの下判定ライン

float gyroX = 0.0f;
float gyroY = 0.0f;
float gyroZ = 0.0f;
int upCount = 0;
bool isShaked = false;

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
  delay(100);
  BTSerial.println("");

}

void read_serial(){
  if(BTSerial.available()){ //Bluetoothデータ受信で
    String msg = BTSerial.readString(); //BTシリアルで送られてきた文字列をmsgに格納
    M5.Lcd.println("Received: " + msg); //M5Stickの画面に文字列msgを表示
    //BTSerial.print(msg);  //BTシリアルに受け取ったmsgを送信

  }
}

void getShaked(){
  auto imu_update = M5.Imu.update();
  if(imu_update){
    M5.Imu.getGyro(&gyroX, &gyroY, &gyroZ);

    if(gyroX >= UPPER_LIMIT){
      upCount++;
    }

    if(gyroX <= LOWER_LIMIT && upCount > 0){
      upCount = 0;
      isShaked = true;
    }

    if(isShaked){
      BTSerial.println("Shaked");
      delay(100);
      BTSerial.println("");
      isShaked = false;
    }
    else { return; }
  }
}

void setup() {
  // put your setup code here, to run once:
  M5.begin();
  M5.Imu.init();
  M5.Lcd.setTextFont(1);
  M5.Lcd.setCursor(0, 0, 2);
  M5.Lcd.println("BTSerial, RFID, sample! :D");
  BTSerial.begin("M5StickCPlus2BT");
  M5.Ex_I2C.begin();
  mfrc522_i2c.PCD_Init();
}

void loop() {
  // put your main code here, to run repeatedly:
  read_rfid();
  read_serial();
  getShaked();
}

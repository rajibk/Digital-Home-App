// Sample Arduino Json Web Client
// Downloads and parse http://jsonplaceholder.typicode.com/users/1
//
// Copyright Benoit Blanchon 2014-2017
// MIT License
//
// Arduino JSON library
// https://github.com/bblanchon/ArduinoJson
// If you like this project, please add a star!

#include <ArduinoJson.h>
#include <Ethernet.h>
#include <SPI.h>


EthernetClient client;

const char* server = "digital-home.azurewebsites.net";  // server's address
const char* resource = "/api/DigitalApplianceAPI/GetAppStatusARDUNO?Email=rajibkanungo@gmail.com";                    // http resource
const unsigned long BAUD_RATE = 9600;                 // serial connection speed
const unsigned long HTTP_TIMEOUT = 10000;  // max respone time from server
const size_t MAX_CONTENT_SIZE = 512;       // max size of the HTTP response
const int NUM_LEDS = 13; //Total number of LED PINs

// ARDUINO entry point #1: runs once when you press reset or power the board
//Reset the 13 output pins
void setup() {

  //pinMode(1, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(2, OUTPUT);  /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(3, OUTPUT);  /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  pinMode(4, OUTPUT); 
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  //pinMode(8, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(9, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(10, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(11, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(12, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //pinMode(13, OUTPUT); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/

  delay(100);

  //digitalWrite(1, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(2, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(3, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  digitalWrite(4, LOW);
  digitalWrite(5, LOW);
  digitalWrite(6, LOW);
  digitalWrite(7, LOW);
  //digitalWrite(8, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(9, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(10, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(11, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(12, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  //digitalWrite(13, LOW); /*Commemted knowingly..can be uncommented anytime to extend to other pins**/
  
  
  
  initSerial();
  initEthernet();
  //Set the digital pins as output as well as set their current status to LOW

  

   //digitalWrite(9, HIGH);
      
}

// ARDUINO entry point #2: runs over and over again forever

void loop() {
  //return;
  if (connect(server)) {
    Serial.println("A : Connected to" );
    Serial.println(server);
    if (sendRequest(server, resource) && skipResponseHeaders()) {

      if (readReponseContent()) {
        //printUserData(&userData);
      }
    }
  }
  disconnect();
  wait();
}

// Initialize Serial port
void initSerial() {
  Serial.begin(BAUD_RATE);
  while (!Serial) {
    ;  // wait for serial port to initialize
  }
  Serial.println("Serial ready");
}

// Initialize Ethernet library
void initEthernet() {
  byte mac[] = {0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED};
  if (!Ethernet.begin(mac)) {
    Serial.println("Failed to configure Ethernet");
    return;
  }
  Serial.println("Ethernet ready");
  delay(1000);
}

// Open connection to the HTTP server
bool connect(const char* hostName) {
  Serial.print("Connect to ");
  Serial.println(hostName);

  bool ok = client.connect(hostName, 80);

  Serial.println(ok ? "Connected" : "Connection Failed!");
  return ok;
}

// Send the HTTP GET request to the server
bool sendRequest(const char* host, const char* resource) {
  Serial.print("B GET ");
  Serial.println(resource);

  client.print("GET ");
  client.print(resource);
  client.println(" HTTP/1.0");
  client.print("Host: ");
  client.println(host);
  client.println("Connection: close");
  client.println();

  return true;
}

// Skip HTTP headers so that we are at the beginning of the response's body
bool skipResponseHeaders() {
  // HTTP headers end with an empty line
  char endOfHeaders[] = "\r\n\r\n";

  client.setTimeout(HTTP_TIMEOUT);
  bool ok = client.find(endOfHeaders);
  //return true;
  if (!ok) {
    Serial.println("No response or invalid response!");
  }
  Serial.println("Valid Response!");
  return ok;
}


bool readReponseContent() {
  // Compute optimal size of the JSON buffer according to what we need to parse.
  // This is only required if you use StaticJsonBuffer.

  // take your JSON file and paste it in the https://arduinojson.org/v5/assistant/ to know the size.
  // I have used 13 elements in the jSON file as ARDUINO UNO has 13 digital output pins only
  const size_t BUFFER_SIZE = JSON_OBJECT_SIZE(13) + 110;  
   //   + MAX_CONTENT_SIZE;    // additional space for strings

  // Allocate a temporary memory pool
  DynamicJsonBuffer jsonBuffer(BUFFER_SIZE);

  JsonObject& root = jsonBuffer.parseObject(client);

  if (!root.success()) {
    Serial.println("JSON parsing failed!");
    return false;
  }

 const char* appstatus[13];

 appstatus[0] = root["A1"]; // 1
 appstatus[1] = root["A2"]; // "O"
 appstatus[2] = root["A3"]; // "O"
 appstatus[3] = root["A4"]; // "O"
 appstatus[4] = root["A5"]; // "O"
 appstatus[5] = root["A6"]; // "O"
 appstatus[6] = root["A7"]; // "O"
 appstatus[7] = root["A8"]; // "O"
 appstatus[8] = root["A9"]; // "O"
 appstatus[9] = root["A10"]; // "O"
 appstatus[10] = root["A11"]; // "O"
 appstatus[11] = root["A12"]; // "O"
 appstatus[12] = root["A13"]; // "O"
 
 setPinStatus(appstatus);

//userData.A1 = root["A1"];

//Serial.println("M");
// Serial.println(appstatus[0]);
// Serial.println(appstatus[1]);
//Serial.println("L");
}

// Print the data extracted from the JSON
void setPinStatus(const char* AppStatus[]) {
    //All the 13 PINs can be configured as output..I have here confured only 4 to 7 as output; 
    //The code can be extendeble to max 13 PINs
    //However, some checks need to be done as the PIN1, PIN 10 and 11 seems to be high all the time, despite digitalwrite to LOW.
    //pin 1

    if (*AppStatus[0] == 'O')
      { digitalWrite(4, HIGH); }
    else
      { digitalWrite(4, LOW); }

      //pin 2
    if (*AppStatus[1] == 'O')
      { digitalWrite(5, HIGH); }
    else
      { digitalWrite(5, LOW); }

      //pin 3
    if (*AppStatus[2] == 'O')
      { digitalWrite(6, HIGH); }
    else
      { digitalWrite(6, LOW); }

      //pin 4
    if (*AppStatus[3] == 'O')
      { digitalWrite(7, HIGH); }
    else
      { digitalWrite(7, LOW); }

     //Commented deliberately..it can be uncommented and extended to all the 13 pins
      //pin 5
      /*
    if (*AppStatus[4] == 'O')
      { digitalWrite(5, HIGH); }
    else
      { digitalWrite(5, LOW); }

      //pin 6
    if (*AppStatus[5] == 'O')
      { digitalWrite(6, HIGH); }
    else
      { digitalWrite(6, LOW); }

      //pin 7
    if (*AppStatus[6] == 'O')
      { digitalWrite(7, HIGH); }
    else
      { digitalWrite(7, LOW); }

      //pin 8
    if (*AppStatus[7] == 'O')
      { Serial.println("8-High");
        Serial.println(AppStatus[7]);
        digitalWrite(8, HIGH); }
    else
      { Serial.println("8-Low");
        Serial.println(AppStatus[7]);
        digitalWrite(8, LOW); }

      //pin 9
    if (*AppStatus[8] == 'O')
      { Serial.println("9-High");
        digitalWrite(9, HIGH); }
    else
      { Serial.println("9-Low");
        digitalWrite(9, LOW); }

      //pin 10
    if (*AppStatus[9] == 'O')
      { Serial.println("10-High");
        digitalWrite(10, HIGH); }
    else
      { Serial.println("10-Low");
        digitalWrite(10, LOW); 
        digitalWrite(10, LOW);}

      //pin 11
    if (*AppStatus[10] == 'O')
      { Serial.println("11-High");
        digitalWrite(11, HIGH); }
    else
      { Serial.println("12-Low");
        digitalWrite(11, LOW); 
        digitalWrite(11, LOW);
        }

      //pin 12
    if (*AppStatus[11] == 'O')
      { digitalWrite(12, HIGH); }
    else
      { digitalWrite(12, LOW); }

      //pin 13
    if (*AppStatus[12] == 'O')
      { digitalWrite(13, HIGH); } 
    else
      { digitalWrite(13, LOW); }
      comments ended */
}

// Close the connection with the HTTP server
void disconnect() {
  Serial.println("Disconnect");
  client.stop();
}

// Pause for a 1 minute
void wait() {
  Serial.println("Wait 20 seconds");
  delay(5000);
}

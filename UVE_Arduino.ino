// Declarando as entradas dos sensores
#define BUZZ 13
#define BUZZ2 9
#define AZUL 12
#define VERMELHO 11
#define VERDE 10
#define SENSORUV A0
#define SENSORTEMP A1
 
 
// Armazena o que foi lido da porta analógica
int RECEBE_SENSOR = 0;
 
//Armazenar conversões
int TENSAO = 0;
int ULTIMA_TENSAO = TENSAO;

unsigned long tempoAnterior = 0; 
const long intervalo = 10000;

String indice = "0";
 
 
//-----------------------------//
 
void setup() {
  //buzzer e leds
  pinMode(BUZZ, OUTPUT);
  pinMode(BUZZ2, OUTPUT);
  pinMode(AZUL, OUTPUT);
  pinMode(VERMELHO, OUTPUT);
  pinMode(VERDE, OUTPUT);
 
  //sensores
  pinMode(SENSORUV, INPUT);
 
  //Serial
  Serial.begin(9600);
}
 
void Portas_TM(int call, int a, int v) {
 
  digitalWrite(BUZZ, call);
  digitalWrite(AZUL, a);
  digitalWrite(VERMELHO, v);
 
  if (call == HIGH) {
    tone(BUZZ, 261);
  } else {
    noTone(BUZZ);
  }
}
 
void Portas_UV(int call2, int ve) {
 
  digitalWrite(BUZZ2, call2);
  digitalWrite(VERDE, ve);
 
  if (call2 == HIGH) {
    tone(BUZZ2, 349);
  } else {
    noTone(BUZZ2);
  }
}
 
void loop() {
  unsigned long tempoAtual = millis();
  
  //receber portas analogicas do sensor de temperatura
  float RECEBE_ST = float(analogRead(SENSORTEMP));  // Obtém o valor analógico que varia de 0 a 1023
  float TENSAO_T = (RECEBE_ST * 5) / 1023;          // Vamos converter esse valor para tensão elétrica
  float TEMPERATURA = TENSAO_T / 0.010;             // dividimos a tensão por 0.010 que representa os 10 mV
 
  //receber portas analogicas do sensor UV
  RECEBE_SENSOR = analogRead(SENSORUV);
  TENSAO = (RECEBE_SENSOR * (5 / 1023.0)) * 4000;
 
  //avisar repasse
  if (tempoAtual - tempoAnterior >= intervalo) {
  
    tempoAnterior = tempoAtual;
  
    for (int i = 0; i < 3; i++) {
    Portas_UV(HIGH, HIGH);
    delay(210);
    Portas_UV(LOW, LOW);
    delay(100);  
    }
  }


  if (TENSAO <= 227) {
    indice = "0";
  }
  else if (TENSAO > 227 && TENSAO <= 318) {
    indice = "1";
  } else if (TENSAO > 318 && TENSAO <= 408) {
    indice = "2";
  } else if (TENSAO > 408 && TENSAO <= 503) {
    indice = "3";
  } else if (TENSAO > 503 && TENSAO <= 606) {
    indice = "4";
  } else if (TENSAO > 606 && TENSAO <= 696) {
    indice = "5";
  } else if (TENSAO > 696 && TENSAO <= 795) {
    indice = "6";
  }else if (TENSAO > 795 && TENSAO <= 881) {
    indice = "7";
  } else if (TENSAO > 881 && TENSAO <= 976) {
    indice = "8";
  } else if (TENSAO > 976 && TENSAO <= 1079) {
    indice = "9";
  } else if (TENSAO > 1079 && TENSAO <= 1170) {
    indice = "10";
  } else if (TENSAO > 1170 && TENSAO <= 1270) {
    indice = "11";
  } else  {
    indice = "12";
  }
 
  //Ocorrencias variacao de temperatura
  if (TEMPERATURA < 25) {
 
    for (int i = 0; i < 2; i++) {
      // Ligar o buzzer
      Portas_TM(HIGH, HIGH, LOW);
      delay(100);  // Tempo que o buzzer fica ligado (em milissegundos)
      // Desligar o buzzer
      Portas_TM(LOW, HIGH, LOW);
      delay(100);  // Tempo que o buzzer fica desligado (em milissegundos)
    }
 
  } else if (TEMPERATURA >= 25 && TEMPERATURA < 28) {
    Portas_TM(LOW, LOW, LOW);
    delay(3000);
  } else if (TEMPERATURA >= 28) {
 
    for (int i = 0; i < 2; i++) {
      // Ligar o buzzer
      Portas_TM(HIGH, LOW, HIGH);
      delay(100);  // Tempo que o buzzer fica ligado (em milissegundos)
      // Desligar o buzzer
      Portas_TM(LOW, LOW, HIGH);
      delay(100);  // Tempo que o buzzer fica desligado (em milissegundos)
    }
  }
 
 
  //mostrar
  if (isnan(TENSAO)) {
    Serial.println("Failed to read");
  } else  //if(TENSAO!=ULTIMA_TENSAO)
  {
    //ULTIMA_TENSAO=TENSAO;
    //Serial.println(TENSAO);
 
    String temper = String(TEMPERATURA);
    Serial.println("i" + indice);
    delay(100);
    Serial.println("t" + temper);
  }
  delay(3000);
}
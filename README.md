# Minigame "Tanks" - Laughing Fiesta Project
---


## Zasady Gry
Jeździsz czołgiem i napierdalasz w innych graczy

### Aktywne Jednostki
- czołg głównego gracza
- wrogie czołgi sterowanie przez graczy lub AI

### Sterowanie Gracza:
-   poruszanie się czołgiem - lewy joystick 
-   poruszanie się lufą czołgu - prawy joystick
-   podwójny klik - wystrzał pocisku

### Właściwości Czołgu:
-   każdy czołg posiada 3 życia, które na rozgrywkę. Po utracie ostatniego nie może już powrócić do gry
-   2 rodzaje pocisków zależne od ulepszenia
-   prędkość poruszania się

### Pocisk rodzaje:
1.   generuje wybuch, który wpływa tylko na przeszkodę w którą trafił
2.   generuje wybuch, który wpływa na wszystkie ściany wokół uderzenia pocisku

### Przeszkody Funkcje:
-    ceglany blok - rozwala się po kontakcie z pociskiem. Wypadają z niego ulepszenia
-    gruby ceglany blok - potrzebuje 2 trafień pocisku by został rozwalony. Wypadają z niego ulepszenia
-    utwardzony ceglany blok - potrzebuje 3 trafień pocisku by został rozwalony. Wypadają z niego ulepszenia
-    stalowy blok - niezniszczalny

### Ulepszenia Czołgów
-    ulepszone pociski do rodzaju 2
-    zwiększona prędkość
-    osłona - zostaje ona usunięta po kontakcie z pociskiem
-    nietykalność - przez 10 sekund czołg jest niezniszczalny

### Rozgrywka
-   każdy gracz zaczyna z swojego punktu startowego, który jest osadzony na rogu mapy(jak na razie będziemy mieć tylko 4 punkty startowe)
-   każdy gracz niszczy przeszkody by dostać się do wroga i także by zdobyć jakieś ulepszenia
-   po każdym zabójstwie gracz jest respiony w swoim punkcie startowym z 5 sekundową nietykalnością.
-   rozgrywka trwa dopóki nie zostanie ostatni czołg na polu bitwy
-   jeżeli gra się wydłuży zostanie zastosowana strefa, czołgi poza nią będą tracić życie
-   strefa będzie się zmniejszać wraz z czasem.
-   Strefa nie będzie zwykłym kołem które się zmniejsza wraz z czasem, tylko niezniszczalnymi przeszkodami, które będą zlatywać z nieba i niszczyć czołgi na swojej drodze
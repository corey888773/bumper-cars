from PIL import Image
import math
import numpy as np

sciezka_wej = input("Podaj sciezke do pliku wejsciowego: ")

try:
    plik_wej = Image.open(sciezka_wej).convert('RGBA')
except:
    print("Nieprawidlowa sciezka do pliku wejsciowego!")
    exit(-1)

sciezka_wyj = input("Podaj sciezke do pliku wyjsciowego: ")

try:
    r1, r2, alfa, beta = int(input("Podaj rozmiar wewnetrzny: ")), int(input("Podaj rozmiar zewnetrzny: ")), float(input("Podaj kat w stopniach do obrotu: ")) / 180 * math.pi, float(input("Podaj kat w stopniach do wyciecia: ")) / 180 * math.pi
except:
    print("Bledny rozmiar")
    exit(-1)

if r1 < 0 or r1 >= r2 or 0 >= beta or beta > math.pi:
    print("Bledny rozmiar")
    exit(-1)

nowy_rozmiar = r2 * 2 + 1

if plik_wej.size[0] < nowy_rozmiar or plik_wej.size[1] < nowy_rozmiar:
    print("Bledny rozmiar")
    exit(-1)

nowy_obraz = Image.new("RGBA", (nowy_rozmiar, nowy_rozmiar))
piksele_wej = plik_wej.load()
piksele_wyj = nowy_obraz.load()

cos_beta = math.cos(beta)

cos_alfa, sin_alfa = math.cos(alfa), math.sin(alfa)

xs, ys = plik_wej.size[0] // 2, plik_wej.size[1] // 2

xl, yl = xs - r2, ys - r2

for x in range(nowy_rozmiar):
    for y in range(nowy_rozmiar):
        x_stary_obraz, y_stary_obraz = xl + x, yl + y
        odl = math.sqrt((xs - x_stary_obraz) ** 2 + (ys - y_stary_obraz) ** 2)
        if r1 <= odl <= r2 and cos_alfa * (x_stary_obraz - xs) + sin_alfa * (y_stary_obraz - ys) >= odl * cos_beta:
            piksele_wyj[x, y] = piksele_wej[x_stary_obraz, y_stary_obraz]
        else:
            piksele_wyj[x, y] = (0, 0, 0, 0)

plik_wej.close()

nowy_obraz.save(sciezka_wyj, format = "png")
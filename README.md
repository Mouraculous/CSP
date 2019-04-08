# CSP

Zbiór zmiennych v = {v1, v2, ..., vn}
Zbiór dziedzin D = {d1, d2, ..., dn}, tyle ile zmiennych, połączone w pary
Di = {v1(i), v2(i), ..., vm(i)} - wartości
vi E {Di}

Ograniczenia - funkcje: Pi 

Przykład: Sudoku

Kratka - zmienna v
Dziedzina {1, 2, 3, 4, 5, 6, 7, 8, 9}
Ograniczenia = Kolumna unikalna, wiersz unikalny, kwadraty 3x3 unikalne => 3 dla każdej kratki(zmiennej)

Przeszukiwanie drzewa:

V1 v1(1), v2(1), ..., vn(1)
V2 v1(2), v2(2), ..., vn(2)


Do sprawka liczba nawrotów i odwiedzone węzły. W części teoretycznej opisać heurystyki. Jeśli długo trwa, opisać, że ustalono limit czasowy i algorytm go przekroczył. OPISAĆ OSI NA WYKRESACH.

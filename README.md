**zadanie 1.

Kod służy do pobierania danych z zewnętrznego źródła i parsowania go do podanego typu.

Linia 2, 3, 4: Powtórzenie kodu.
Linia 14: Nieużywana zmienna ‘Variable1’, można ją usunąć.
Lina 24: Konstruktor nie przypisuje wartości do ‘_httpClientProxy’.
Linia 29: Metoda ‘Handle’ przyjmuje zbyt wiele argumentów i zajmuje się zbyt wieloma rzeczami.
	Metoda przyjmuje argument ‘request’ i z niego nie korzysta.
	Typ argumentu ‘payload’ można zmienić z ‘object’ na ‘HttpContent’.
	Należy zmienić nazwę argumentu ‘pleaseProvideFullUrlHere’ na np. ‘FullUrl’.
Linia 33: Metoda ‘Handle’ przyjmuje jako argument FullUrl a następnie tworzy ‘uriString’ przez sklejenie bazowego adresu i całkowitego adresu. Powstanie np. ‘https://example/https://example/something’.
Linia 37, 47: Zamienić if elsy na if ze znakiem !=.
Linia 45: Niepotrzebna zmienna ‘headers’, zamiast niej można używać zmiennej ‘aditionalHeaders’.
Linia 58: ‘_httpClientProxy’ nie jest nigdzie przypisywany i może być null.
Linia 62: Nieprawidłowy Exception type. Zamieniłbym na ‘HttpRequestException’.
Linia 77: Nazwa ‘ParseAsync’ sugeruje że metoda jest asynchroniczna, a nie jest.


**Użycie aplikacjji.

- Aplikacja służy do pobierania i wyświetlania danych dotyczących kursu walut z API narodowego banku polskiego.
- Aby uzyskać inforamcje dotyczące kursu walut z konkretnego dnia roboczego podaj datę w formacie yyyy-MM-dd np. '2020-08-18'.
- Aby opuścić aplikację wpisz 'escape'.

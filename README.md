# BCrypt.Net-Next / BCrypt

BCrypt.Net-Next — это библиотека для C# / .NET. bcrypt — адаптивная криптографическая хеш-функция формирования ключа, используемая для защищенного хранения паролей. Функция основана на шифре Blowfish.

BCrypt.Net-Next использует bcrypt как алгоритм хэширования паролей. Защищает данные, добавляя случайную «соль» к каждому паролю и замедляя процесс вычисления. Функция автоматически встраивает соль в хеш, делает перебор (brute-force) неэффективным за счет высокой вычислительной сложности, а также позволяет повышать защиту со временем.

## Методы библиотеки

```csharp
// Хэширование пароля (автоматическая генерация соли)
string hash = BCrypt.Net.BCrypt.HashPassword("password");

// Хэширование с указанием work factor (10 = 2^10 = 1024 раунда)
string hash = BCrypt.Net.BCrypt.HashPassword("password", 10);

// Хэширование с указанием соли (для специальных случаев)
string hash = BCrypt.Net.BCrypt.HashPassword("password", salt);

// Проверка пароля
bool isValid = BCrypt.Net.BCrypt.Verify("password", hash);

// Генерация соли (при необходимости)
string salt = BCrypt.Net.BCrypt.GenerateSalt();
string salt = BCrypt.Net.BCrypt.GenerateSalt(12);


Формат хэша
$2a$10$N9qo8uLOickgx2ZMRZoMy.Mr/FuX7K2vD/hTp9GgX8L.kP8Yx3tS2
Часть                              Обозначение
$2a$	                           Версия алгоритма ($2a$, $2b$, $2y$)
10	                               Стоимость (2^10 = 1024 итерации/раунда)
N9qo8uLOickgx2ZMRZoMy	           Соль (22 символа в base64)
Mr/FuX7K2vD/hTp9GgX8L.kP8Yx3tS2    Хэш (31 символ)



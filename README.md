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

Параметр стоимости (work factor / cost)
Work factor определяет количество раундов хэширования: 2^work_factor. Каждое увеличение на 1 удваивает время вычисления.
<img width="772" height="311" alt="image" src="https://github.com/user-attachments/assets/c4f7994a-b754-4aec-9d08-227a3202efb7" />

Как библиотека использует алгоритм

BCrypt.Net-Next реализует следующие этапы:
Хэширование (HashPassword)
public static string HashPassword(string password, int workFactor)
{
    // 1. Генерация случайной соли (16 байт)
    string salt = GenerateSalt(workFactor);
    
    // 2. Вызов криптографического ядра BCrypt
    string hash = CryptRaw(password, salt, workFactor);
    
    // 3. Возврат строки в формате: $2a$ww$salt+hash
    return hash;
}

Верификация (Verify)
public static bool Verify(string password, string hash)
{
    // 1. Извлечение соли из строки хэша
    string salt = ExtractSalt(hash);
    
    // 2. Повторное хэширование пароля с извлеченной солью
    string testHash = HashPassword(password, salt);
    
    // 3. Сравнение полученного хэша с сохраненным
    return SlowEquals(hash, testHash);
}

Важно: функция Verify НЕ расшифровывает хэш. Она заново вычисляет хэш пароля, используя ту же соль, и сравнивает результаты. Это единственно возможный способ проверки, так как хэш-функции необратимы.

Особенности реализации BCrypt.Net-Next
Использует безопасный генератор случайных чисел (RNGCryptoServiceProvider) для создания соли

Реализует защищенное сравнение строк (time-constant), исключающее timing-атаки

Поддерживает все версии BCrypt-хэшей ($2a$, $2b$, $2y$)

Запуск программы с BCrypt.Net-Next
Системные требования
Windows 7 / 8 / 10 / 11, Linux, macOS

.NET SDK 6.0 или выше (рекомендуется .NET 8.0)

Visual Studio 2022 / VS Code / любой текстовый редактор

100 MB свободного дискового пространства


Установка библиотеки BCrypt.Net-Next
В меню выбрать "Проект" -> "Управление пакетами NuGet"

Перейти на вкладку "Обзор"

В строку поиска ввести "BCrypt.Net-Next"

Выбрать пакет с автором "bcrypt-net-next"

Нажать кнопку "Установить"

Подтвердить установку

Альтернативный способ через консоль диспетчера пакетов
Открыть "Сервис" -> "Диспетчер пакетов NuGet" -> "Консоль диспетчера пакетов"

Ввести команду: Install-Package BCrypt.Net-Next

Нажать Enter


Заключение
Библиотека инкапсулирует сложность алгоритма, предоставляя разработчику два основных метода: HashPassword для создания хэша и Verify для проверки пароля. Work factor управляет вычислительной сложностью, что позволяет сохранять безопасность системы даже при увеличении производительности атакующего оборудования.

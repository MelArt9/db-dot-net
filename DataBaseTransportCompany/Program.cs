using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataBaseTransportCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TransportCompanyContext context = new TransportCompanyContext())
            {
                // Проверяем доступность базы данных
                bool isDatabaseAvailable = context.Database.CanConnect();
                if (isDatabaseAvailable)
                {
                    Console.WriteLine("База данных доступна.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("База данных не доступна.");
                    return; // Выходим из программы, так как база данных недоступна
                }

                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine();
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Создать запись");
                    Console.WriteLine("2. Получить записи");
                    Console.WriteLine("3. Обновить запись");
                    Console.WriteLine("4. Удалить запись");
                    Console.WriteLine("5. Выполнить запрос к базе данных");
                    Console.WriteLine("0. Выйти из программы");

                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();

                    Console.Clear();

                    switch (choice)
                    {
                        case "1":
                            CreateRecord(context);
                            break;
                        case "2":
                            GetRecords(context);
                            break;
                        case "3":
                            UpdateRecord(context);
                            break;
                        case "4":
                            DeleteRecord(context);
                            break;
                        case "5":
                            ExecuteQuery(context);
                            break;
                        case "0":
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Некорректный ввод.");
                            break;
                    }
                }
            }
        }

        static void CreateRecord(TransportCompanyContext context)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите таблицу, в которой нужно создать запись:");
                Console.WriteLine("1. Марка");
                Console.WriteLine("2. Модель");
                Console.WriteLine("3. Тип");
                Console.WriteLine("4. Транспортное средство");
                Console.WriteLine("5. Транспорт_водитель");
                Console.WriteLine("6. Водитель");
                Console.WriteLine("7. Водитель_категория");
                Console.WriteLine("8. Категория");
                Console.WriteLine("0. Вернуться в предыдущее меню");

                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        CreateStamp(context);
                        break;
                    case "2":
                        CreateModel(context);
                        break;
                    case "3":
                        CreateType(context);
                        break;
                    case "4":
                        CreateTransportVehicle(context);
                        break;
                    case "5":
                        CreateTransportVehicleDriver(context);
                        break;
                    case "6":
                        CreateDriver(context);
                        break;
                    case "7":
                        CreateDriverCategory(context);
                        break;
                    case "8":
                        CreateCategory(context);
                        break;
                    case "0":
                        return; // Возврат в предыдущее меню
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите одну из доступных опций.");
                        break;
                }
            }
        }

        static void CreateStamp(TransportCompanyContext context)
        {
            int stampId;
            string title;

            do
            {
                Console.Write("Введите ID марки: ");
            } while (!int.TryParse(Console.ReadLine(), out stampId));

            do
            {
                Console.Write("Введите название марки: ");
                title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Название марки не может быть пустым. Пожалуйста, введите корректное название.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            // Проверка, существует ли марка с указанным ID
            var existingStamp = context.Stamps.FirstOrDefault(s => s.stampId == stampId);

            if (existingStamp != null)
            {
                Console.WriteLine($"Марка с ID {stampId} уже существует (название: {existingStamp.title}).");
            }
            else
            {
                // Создание новой марки с указанным ID и названием
                var newStamp = new StampTransport { stampId = stampId, title = title };
                context.Stamps.Add(newStamp);
                context.SaveChanges();

                Console.WriteLine($"Марка с ID {stampId} и названием '{title}' успешно создана.");
            }

            Console.WriteLine();
        }

        static void CreateModel(TransportCompanyContext context)
        {
            int modelId;
            while (true)
            {
                Console.Write("Введите ModelId: ");
                if (int.TryParse(Console.ReadLine(), out modelId))
                {
                    if (context.Models.Any(m => m.modelId == modelId))
                    {
                        Console.WriteLine("Модель с указанным ModelId уже существует в базе данных.");
                        return; // Возвращаемся, чтобы не добавлять существующую модель
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат ModelId.");
                }
            }

            string title = string.Empty;
            while (string.IsNullOrWhiteSpace(title))
            {
                Console.Write("Введите название модели: ");
                title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Название модели не может быть пустым.");
                }
            }

            int stampId;
            while (true)
            {
                Console.Write("Введите StampId: ");
                if (int.TryParse(Console.ReadLine(), out stampId))
                {
                    if (context.Stamps.Any(s => s.stampId == stampId))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Марка с указанным StampId не найдена в базе данных.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат StampId.");
                }
            }

            var newModel = new ModelTransport { modelId = modelId, title = title.Trim(), stampId = stampId };
            context.Models.Add(newModel);

            try
            {
                context.SaveChanges();
                Console.WriteLine("Модель успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании модели: {ex.Message}");
            }
            Console.WriteLine();
        }

        static void CreateType(TransportCompanyContext context)
        {
            int typeId;
            string title;

            do
            {
                Console.Write("Введите ID типа: ");
            } while (!int.TryParse(Console.ReadLine(), out typeId));

            do
            {
                Console.Write("Введите название типа: ");
                title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Название типа не может быть пустым. Пожалуйста, введите корректное название.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            // Проверка, существует ли тип с указанным ID
            var existingType = context.Types.FirstOrDefault(t => t.typeId == typeId);

            if (existingType != null)
            {
                Console.WriteLine($"Тип с ID {typeId} уже существует (название: {existingType.title}).");
            }
            else
            {
                // Создание нового типа с указанным ID и названием
                var newType = new TypeTransport { typeId = typeId, title = title };
                context.Types.Add(newType);
                context.SaveChanges();

                Console.WriteLine($"Тип с ID {typeId} и названием '{title}' успешно создан.");
            }

            Console.WriteLine();
        }

        static void CreateTransportVehicle(TransportCompanyContext context)
        {
            int transportVehicleId;
            string number;

            do
            {
                Console.Write("Введите ID транспортного средства: ");
            } while (!int.TryParse(Console.ReadLine(), out transportVehicleId));

            while (true)
            {
                Console.Write("Введите номер: ");
                number = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(number))
                {
                    Console.WriteLine("Номер не может быть пустым. Пожалуйста, введите корректный номер.");
                }
                else
                {
                    break;
                }
            }

            // Проверка, существует ли транспортное средство с указанным ID
            var existingTransportVehicle = context.TransportVehicles.FirstOrDefault(tv => tv.transportVehicleId == transportVehicleId);

            if (existingTransportVehicle != null)
            {
                Console.WriteLine($"Транспортное средство с ID {transportVehicleId} уже существует (номер: {existingTransportVehicle.number}).");
            }
            else
            {
                int codeRegion;
                while (true)
                {
                    Console.Write("Введите код региона: ");
                    if (!int.TryParse(Console.ReadLine(), out codeRegion) || codeRegion <= 0)
                    {
                        Console.WriteLine("Неверный формат кода региона. Код региона должен быть положительным числом.");
                    }
                    else
                    {
                        break;
                    }
                }

                DateOnly? dateLastInspection = null;
                while (true)
                {
                    Console.Write("Введите дату последнего ТО (гггг-мм-дд): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break; // Если пользователь оставил поле пустым, просто выходим из цикла
                    }

                    if (DateOnly.TryParse(input, out DateOnly inspectionDate))
                    {
                        dateLastInspection = inspectionDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат даты последнего ТО. Пожалуйста, введите дату в формате 'гггг-мм-дд' или оставьте поле пустым.");
                    }
                }

                string engineNumber;
                while (true)
                {
                    Console.Write("Введите номер двигателя: ");
                    engineNumber = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(engineNumber))
                    {
                        Console.WriteLine("Номер двигателя не может быть пустым. Пожалуйста, введите корректный номер.");
                    }
                    else
                    {
                        break;
                    }
                }

                int numberSeats;
                while (true)
                {
                    Console.Write("Введите количество посадочных мест: ");
                    if (!int.TryParse(Console.ReadLine(), out numberSeats) || numberSeats <= 0)
                    {
                        Console.WriteLine("Неверный формат количества посадочных мест. Количество посадочных мест должно быть положительным числом.");
                    }
                    else
                    {
                        break;
                    }
                }

                int? numberStandingPlaces = null;
                while (true)
                {
                    Console.Write("Введите количество стоячих мест (если есть): ");
                    string standingPlacesInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(standingPlacesInput))
                    {
                        break;
                    }
                    else if (!int.TryParse(standingPlacesInput, out int standingPlaces) || standingPlaces < 0)
                    {
                        Console.WriteLine("Неверный формат количества стоячих мест. Количество стоячих мест должно быть неотрицательным числом или оставьте поле пустым.");
                    }
                    else
                    {
                        numberStandingPlaces = standingPlaces;
                        break;
                    }
                }

                int maxSpeed;
                while (true)
                {
                    Console.Write("Введите максимальную скорость (км/ч): ");
                    if (!int.TryParse(Console.ReadLine(), out maxSpeed) || maxSpeed <= 0)
                    {
                        Console.WriteLine("Неверный формат максимальной скорости. Максимальная скорость должна быть положительным числом.");
                    }
                    else
                    {
                        break;
                    }
                }

                DateOnly releaseDate;
                while (true)
                {
                    Console.Write("Введите дату выпуска (гггг-мм-дд): ");
                    if (DateOnly.TryParse(Console.ReadLine(), out releaseDate))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат даты выпуска. Пожалуйста, введите дату в формате 'гггг-мм-дд'.");
                    }
                }

                int modelId;
                while (true)
                {
                    Console.Write("Введите ModelId: ");
                    if (int.TryParse(Console.ReadLine(), out modelId) && modelId > 0)
                    {
                        // Проверяем, существует ли модель с указанным ModelId в базе данных
                        if (context.Models.Any(m => m.modelId == modelId))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Модель с указанным ModelId не найдена в базе данных. Пожалуйста, введите корректный ModelId.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ModelId. Пожалуйста, введите положительное целое число.");
                    }
                }

                int typeId;
                while (true)
                {
                    Console.Write("Введите TypeId: ");
                    if (int.TryParse(Console.ReadLine(), out typeId) && typeId > 0)
                    {
                        // Проверяем, существует ли тип с указанным TypeId в базе данных
                        if (context.Types.Any(t => t.typeId == typeId))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Тип с указанным TypeId не найден в базе данных. Пожалуйста, введите корректный TypeId.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат TypeId. Пожалуйста, введите положительное целое число.");
                    }
                }

                // Создаем новый объект TransportVehicle и добавляем его в базу данных
                var newTransportVehicle = new TransportVehicle
                {
                    transportVehicleId = transportVehicleId,
                    number = number,
                    codeRegion = codeRegion,
                    dateLastInspection = dateLastInspection,
                    engineNumber = engineNumber,
                    numberSeats = numberSeats,
                    numberStandingPlaces = numberStandingPlaces,
                    maxSpeed = maxSpeed,
                    releaseDate = releaseDate,
                    modelId = modelId,
                    typeId = typeId
                };

                try
                {
                    // Добавляем новый объект в DbSet и сохраняем изменения в базе данных
                    context.TransportVehicles.Add(newTransportVehicle);
                    context.SaveChanges();

                    Console.WriteLine("Запись успешно создана.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании записи: {ex.Message}");
                }
            }

            Console.WriteLine();
        }

        static void CreateTransportVehicleDriver(TransportCompanyContext context)
        {
            Console.WriteLine("Введите данные для новой связи транспортного средства и водителя:");

            // Получите список всех транспортных средств
            var transportVehicles = context.TransportVehicles.ToList();
            Console.WriteLine("Список доступных транспортных средств:");
            foreach (var vehicle in transportVehicles)
            {
                Console.WriteLine($"{vehicle.transportVehicleId}. {vehicle.number}");
            }

            int transportVehicleId;
            while (true)
            {
                Console.Write("Выберите ID транспортного средства: ");
                if (int.TryParse(Console.ReadLine(), out transportVehicleId) && transportVehicles.Any(tv => tv.transportVehicleId == transportVehicleId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ID транспортного средства. Пожалуйста, выберите существующее транспортное средство.");
                }
            }

            // Получите список всех водителей
            var drivers = context.Drivers.ToList();
            Console.WriteLine("Список доступных водителей:");
            foreach (var driver in drivers)
            {
                Console.WriteLine($"{driver.driverId}. {driver.surname} {driver.name}");
            }

            int driverId;
            while (true)
            {
                Console.Write("Выберите ID водителя: ");
                if (int.TryParse(Console.ReadLine(), out driverId) && drivers.Any(d => d.driverId == driverId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ID водителя. Пожалуйста, выберите существующего водителя.");
                }
            }

            // Создаем новую связь TransportVehicleDriver и добавляем ее в базу данных
            var newTransportVehicleDriver = new TransportVehicleDriver
            {
                transportVehicleId = transportVehicleId,
                driverId = driverId
            };

            try
            {
                // Добавляем новую связь в DbSet и сохраняем изменения в базе данных
                context.TransportVehicleDrivers.Add(newTransportVehicleDriver);
                context.SaveChanges();

                Console.WriteLine("Запись успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании записи: {ex.Message}");
            }
        }

        static void CreateDriver(TransportCompanyContext context)
        {
            int driverId;
            string surname;
            while (true)
            {
                Console.Write("Введите ID водителя: ");
                if (int.TryParse(Console.ReadLine(), out driverId))
                {
                    // Проверяем, существует ли водитель с указанным ID в базе данных
                    if (context.Drivers.Any(d => d.driverId == driverId))
                    {
                        Console.WriteLine($"Водитель с ID {driverId} уже существует.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат ID водителя. Пожалуйста, введите целое число.");
                }
            }

            while (true)
            {
                Console.Write("Фамилия: ");
                surname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surname))
                {
                    Console.WriteLine("Фамилия не может быть пустой. Пожалуйста, введите корректную фамилию.");
                }
                else
                {
                    break;
                }
            }

            string name;
            while (true)
            {
                Console.Write("Имя: ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Имя не может быть пустым. Пожалуйста, введите корректное имя.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Отчество: ");
            string patronymic = Console.ReadLine();

            DateOnly dateOfBirth;
            while (true)
            {
                Console.Write("Дата рождения (гггг-мм-дд): ");
                if (DateOnly.TryParse(Console.ReadLine(), out dateOfBirth))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат даты рождения. Пожалуйста, введите дату в формате 'гггг-мм-дд'.");
                }
            }

            string phone;
            while (true)
            {
                Console.Write("Номер телефона: ");
                phone = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phone))
                {
                    Console.WriteLine("Номер телефона не может быть пустым. Пожалуйста, введите корректный номер.");
                }
                else
                {
                    break;
                }
            }

            string driverLicenseNumber;
            while (true)
            {
                Console.Write("Номер водительского удостоверения: ");
                driverLicenseNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(driverLicenseNumber))
                {
                    Console.WriteLine("Номер водительского удостоверения не может быть пустым. Пожалуйста, введите корректный номер.");
                }
                else
                {
                    break;
                }
            }

            DateOnly validityPeriodRights;
            while (true)
            {
                Console.Write("Срок действия водительского удостоверения (гггг-мм-дд): ");
                if (DateOnly.TryParse(Console.ReadLine(), out validityPeriodRights))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат срока действия водительского удостоверения. Пожалуйста, введите дату в формате 'гггг-мм-дд'.");
                }
            }

            DateOnly experience;
            while (true)
            {
                Console.Write("Дата начала стажа (гггг-мм-дд): ");
                if (DateOnly.TryParse(Console.ReadLine(), out experience))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат даты начала стажа. Пожалуйста, введите дату в формате 'гггг-мм-дд'.");
                }
            }

            Console.Write("Адрес: ");
            string address = Console.ReadLine();

            // Создаем новый объект Driver и добавляем его в базу данных
            var newDriver = new Driver
            {
                driverId = driverId,
                surname = surname,
                name = name,
                patronymic = patronymic,
                dateOfBirth = dateOfBirth,
                phone = phone,
                driverLicenseNumber = driverLicenseNumber,
                validityPeriodRights = validityPeriodRights,
                experience = experience,
                address = address
            };

            try
            {
                // Добавляем нового водителя в DbSet и сохраняем изменения в базе данных
                context.Drivers.Add(newDriver);
                context.SaveChanges();

                Console.WriteLine("Запись успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании записи: {ex.Message}");
            }
        }

        static void CreateDriverCategory(TransportCompanyContext context)
        {
            Console.WriteLine("Введите данные для новой категории водителя:");

            // Получите список всех водителей
            var drivers = context.Drivers.ToList();
            Console.WriteLine("Список доступных водителей:");
            foreach (var driver in drivers)
            {
                Console.WriteLine($"{driver.driverId}. {driver.surname} {driver.name}");
            }

            int driverId;
            while (true)
            {
                Console.Write("Выберите ID водителя для категории: ");
                if (int.TryParse(Console.ReadLine(), out driverId) && drivers.Any(d => d.driverId == driverId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ID водителя. Пожалуйста, выберите существующего водителя.");
                }
            }

            // Получите список всех категорий
            var categories = context.Categories.ToList();
            Console.WriteLine("Список доступных категорий:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.categoryId}. {category.title}");
            }

            int categoryId;
            while (true)
            {
                Console.Write("Выберите ID категории: ");
                if (int.TryParse(Console.ReadLine(), out categoryId) && categories.Any(c => c.categoryId == categoryId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ID категории. Пожалуйста, выберите существующую категорию.");
                }
            }

            // Создаем новый объект DriverCategory и добавляем его в базу данных
            var newDriverCategory = new DriverCategory
            {
                driverId = driverId,
                categoryId = categoryId
            };

            try
            {
                // Добавляем новую категорию водителя в DbSet и сохраняем изменения в базе данных
                context.DriverCategories.Add(newDriverCategory);
                context.SaveChanges();

                Console.WriteLine("Запись успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании записи: {ex.Message}");
            }
        }

        static void CreateCategory(TransportCompanyContext context)
        {
            int categoryId;
            string title;
            while (true)
            {
                Console.Write("Введите ID категории: ");
                if (int.TryParse(Console.ReadLine(), out categoryId))
                {
                    // Проверяем, существует ли категория с указанным ID в базе данных
                    if (context.Categories.Any(c => c.categoryId == categoryId))
                    {
                        Console.WriteLine($"Категория с ID {categoryId} уже существует.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат ID категории. Пожалуйста, введите целое число.");
                }
            }

            while (true)
            {
                Console.Write("Наименование категории: ");
                title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Наименование категории не может быть пустым. Пожалуйста, введите корректное наименование.");
                }
            }

            // Создаем новый объект Category и добавляем его в базу данных
            var newCategory = new Category
            {
                categoryId = categoryId,
                title = title
            };

            try
            {
                // Добавляем новую категорию в DbSet и сохраняем изменения в базе данных
                context.Categories.Add(newCategory);
                context.SaveChanges();

                Console.WriteLine("Запись успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании записи: {ex.Message}");
            }
        }

        static void DisplayRecords<T>(TransportCompanyContext context, DbSet<T> dbSet, Func<T, string> formatFunc, string header) where T : class
        {
            var records = dbSet.ToList();

            Console.WriteLine(header);

            if (records.Count == 0)
            {
                Console.WriteLine("Таблица пуста.");
            }
            else
            {
                foreach (var record in records)
                {
                    Console.WriteLine(formatFunc(record));
                }
            }
            Console.WriteLine();
        }

        static void GetRecords(TransportCompanyContext context)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите таблицу для отображения:");
                Console.WriteLine("1. Марки");
                Console.WriteLine("2. Модели");
                Console.WriteLine("3. Типы транспорта");
                Console.WriteLine("4. Транспортные средства");
                Console.WriteLine("5. Транспорт_водитель");
                Console.WriteLine("6. Водители");
                Console.WriteLine("7. Водитель_категории");
                Console.WriteLine("8. Категории");
                Console.WriteLine("0. Вернуться в предыдущее меню");
                Console.Write("Введите номер таблицы (от 0 до 8): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            DisplayRecords(context, context.Stamps, s => s.ToString(), "Список марок:");
                            break;
                        case 2:
                            DisplayRecords(context, context.Models, m => m.ToString(), "Список моделей:");
                            break;
                        case 3:
                            DisplayRecords(context, context.Types, t => t.ToString(), "Список типов транспорта:");
                            break;
                        case 4:
                            DisplayRecords(context, context.TransportVehicles, tv => tv.ToString(), "Список транспортных средств:");
                            break;
                        case 5:
                            DisplayRecords(context, context.TransportVehicleDrivers, td => td.ToString(), "Список транспортов водителей:");
                            break;
                        case 6:
                            DisplayRecords(context, context.Drivers, d => d.ToString(), "Список водителей:");
                            break;
                        case 7:
                            DisplayRecords(context, context.DriverCategories, dc => dc.ToString(), "Список категорий водителей:");
                            break;
                        case 8:
                            DisplayRecords(context, context.Categories, c => c.ToString(), "Список категорий:");
                            break;
                        case 0:
                            return; // Выход из метода
                        default:
                            Console.WriteLine("Неверный номер таблицы.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат ввода.");
                }
            }
        }

        static void UpdateRecord(TransportCompanyContext context)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите сущность для обновления (введите номер):");
            Console.WriteLine("1. Марка");
            Console.WriteLine("2. Модель");
            Console.WriteLine("3. Тип");
            Console.WriteLine("4. Транспортное средство");
            Console.WriteLine("5. Транспорт_водитель");
            Console.WriteLine("6. Водитель");
            Console.WriteLine("7. Водитель_категория");
            Console.WriteLine("8. Категория");
            Console.WriteLine("0. Вернуться в предыдущее меню");
            Console.Write("Введите номер сущности (от 0 до 8): ");

            if (!int.TryParse(Console.ReadLine(), out int entityNumber))
            {
                Console.WriteLine("Неверный формат ввода. Введите число от 0 до 8.");
                return;
            }

            Console.Clear();

            switch (entityNumber)
            {
                case 1:
                    List<string> updatePropertiesForStamp = new List<string> { "stampId", "title" };
                    UpdateEntity<StampTransport>(context, "Марка", updatePropertiesForStamp);
                    break;
                case 2:
                    List<string> updatePropertiesForModel = new List<string> { "modelId", "title", "stampId" };
                    UpdateEntity<ModelTransport>(context, "Модель", updatePropertiesForModel);
                    break;
                case 3:
                    List<string> updatePropertiesForType = new List<string> { "typeId", "title" };
                    UpdateEntity<TypeTransport>(context, "Тип", updatePropertiesForType);
                    break;
                case 4:
                    List<string> updatePropertiesForTransportVehicle = new List<string> { "transportVehicleId", "number", "codeRegion",
                "dateLastInspection", "engineNumber", "numberSeats", "numberStandingPlaces", "maxSpeed", "releaseDate",
                "modelId", "typeID" };
                    UpdateEntity<TransportVehicle>(context, "Транспортное средство", updatePropertiesForTransportVehicle);
                    break;
                case 5:
                    UpdateTransportVehicleDriver(context);
                    break;
                case 6:
                    List<string> updatePropertiesForDriver = new List<string> { "driverId", "name", "surname", "patronymic",
                "dateOfBirth", "phone", "driverLicenseNumber", "validityPeriodRights", "experience", "address" };
                    UpdateEntity<Driver>(context, "Водитель", updatePropertiesForDriver);
                    break;
                case 7:
                    UpdateDriverCategory(context);
                    break;
                case 8:
                    List<string> updatePropertiesForCategory = new List<string> { "categoryId", "title" };
                    UpdateEntity<Category>(context, "Категория", updatePropertiesForCategory);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный номер сущности. Пожалуйста, введите число от 0 до 8.");
                    break;
            }
        }

        static void UpdateTransportVehicleDriver(TransportCompanyContext context)
        {
            int transportVehicleId;
            int driverId;

            while (true)
            {
                Console.Write("Введите ID Транспортное средство для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out transportVehicleId) || transportVehicleId <= 0)
                {
                    Console.WriteLine("Неверный формат ID Транспортное средство. Пожалуйста, введите корректный ID.");
                    continue;
                }

                Console.Write("Введите ID Водитель для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out driverId) || driverId <= 0)
                {
                    Console.WriteLine("Неверный формат ID Водитель. Пожалуйста, введите корректный ID.");
                    continue;
                }

                // Проверяем, существуют ли такие ID в таблицах
                var transportVehicle = context.Set<TransportVehicle>().Find(transportVehicleId);
                var driver = context.Set<Driver>().Find(driverId);

                if (transportVehicle == null || driver == null)
                {
                    Console.WriteLine("Запись с указанными ID Транспортное средство и/или Водитель не найдена. Пожалуйста, введите существующие ID.");
                    continue;
                }

                // Находим запись по ID
                var entity = context.Set<TransportVehicleDriver>().Find(transportVehicleId, driverId);
                if (entity == null)
                {
                    Console.WriteLine("Запись с указанными ID Транспортное средство и Водитель не найдена. Пожалуйста, введите существующие ID.");
                }
                else
                {
                    // Вывод текущих значений свойств
                    Console.WriteLine();
                    Console.WriteLine("Текущие значения Транспорт_водитель:");
                    Console.WriteLine($"Транспортное средство ID: {entity.transportVehicleId}");
                    Console.WriteLine($"Водитель ID: {entity.driverId}");

                    Console.WriteLine();
                    Console.WriteLine("Введите новые значения для Транспорт_водитель:");

                    string newValue;

                    while (true)
                    {
                        Console.Write("Транспортное средство ID: ");
                        newValue = Console.ReadLine();
                        if (!int.TryParse(newValue, out int newTransportVehicleId) || newTransportVehicleId <= 0)
                        {
                            Console.WriteLine("Неверный формат ID Транспортное средство. Пожалуйста, введите корректный ID.");
                            continue;
                        }
                        // Проверяем существование нового ID в таблице TransportVehicle
                        if (context.Set<TransportVehicle>().Find(newTransportVehicleId) == null)
                        {
                            Console.WriteLine("Транспортное средство с указанным ID не найдено. Пожалуйста, введите существующий ID.");
                            continue;
                        }
                        entity.transportVehicleId = newTransportVehicleId;
                        break;
                    }

                    while (true)
                    {
                        Console.Write("Водитель ID: ");
                        newValue = Console.ReadLine();
                        if (!int.TryParse(newValue, out int newDriverId) || newDriverId <= 0)
                        {
                            Console.WriteLine("Неверный формат ID Водитель. Пожалуйста, введите корректный ID.");
                            continue;
                        }
                        // Проверяем существование нового ID в таблице Driver
                        if (context.Set<Driver>().Find(newDriverId) == null)
                        {
                            Console.WriteLine("Водитель с указанным ID не найден. Пожалуйста, введите существующий ID.");
                            continue;
                        }
                        entity.driverId = newDriverId;
                        break;
                    }

                    // Сохраняем изменения
                    context.SaveChanges();
                    Console.WriteLine("Транспорт_водитель успешно обновлено.");
                    break; // Выход из цикла после успешного обновления
                }
            }
        }

        static void UpdateDriverCategory(TransportCompanyContext context)
        {
            int driverId;
            int categoryId;

            while (true)
            {
                Console.Write("Введите ID Водитель для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out driverId) || driverId <= 0)
                {
                    Console.WriteLine("Неверный формат ID Водитель. Пожалуйста, введите корректный ID.");
                    continue;
                }

                Console.Write("Введите ID Категория для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out categoryId) || categoryId <= 0)
                {
                    Console.WriteLine("Неверный формат ID Категория. Пожалуйста, введите корректный ID.");
                    continue;
                }

                // Проверяем, существуют ли такие ID в таблицах
                var driver = context.Set<Driver>().Find(driverId);
                var category = context.Set<Category>().Find(categoryId);

                if (driver == null || category == null)
                {
                    Console.WriteLine("Запись с указанными ID Водитель и/или Категория не найдена. Пожалуйста, введите существующие ID.");
                    continue;
                }

                // Находим запись по ID
                var entity = context.Set<DriverCategory>().Find(driverId, categoryId);
                if (entity == null)
                {
                    Console.WriteLine("Запись с указанными ID Водитель и Категория не найдена. Пожалуйста, введите существующие ID.");
                }
                else
                {
                    // Вывод текущих значений свойств
                    Console.WriteLine();
                    Console.WriteLine("Текущие значения Водитель_категория:");
                    Console.WriteLine($"Водитель ID: {entity.driverId}");
                    Console.WriteLine($"Категория ID: {entity.categoryId}");

                    Console.WriteLine();
                    Console.WriteLine("Введите новые значения для Водитель_категория:");

                    string newValue;

                    while (true)
                    {
                        Console.Write("Водитель ID: ");
                        newValue = Console.ReadLine();
                        if (!int.TryParse(newValue, out int newDriverId) || newDriverId <= 0)
                        {
                            Console.WriteLine("Неверный формат ID Водитель. Пожалуйста, введите корректный ID.");
                            continue;
                        }
                        // Проверяем существование нового ID в таблице Driver
                        if (context.Set<Driver>().Find(newDriverId) == null)
                        {
                            Console.WriteLine("Водитель с указанным ID не найден. Пожалуйста, введите существующий ID.");
                            continue;
                        }
                        entity.driverId = newDriverId;
                        break;
                    }

                    while (true)
                    {
                        Console.Write("Категория ID: ");
                        newValue = Console.ReadLine();
                        if (!int.TryParse(newValue, out int newCategoryId) || newCategoryId <= 0)
                        {
                            Console.WriteLine("Неверный формат ID Категория. Пожалуйста, введите корректный ID.");
                            continue;
                        }
                        // Проверяем существование нового ID в таблице Category
                        if (context.Set<Category>().Find(newCategoryId) == null)
                        {
                            Console.WriteLine("Категория с указанным ID не найдена. Пожалуйста, введите существующий ID.");
                            continue;
                        }
                        entity.categoryId = newCategoryId;
                        break;
                    }

                    // Сохраняем изменения
                    context.SaveChanges();
                    Console.WriteLine("Водитель_категория успешно обновлено.");
                    break; // Выход из цикла после успешного обновления
                }
            }
        }


        // Метод UpdateEntity остается без изменений
        static void UpdateEntity<T>(TransportCompanyContext context, string entityName, List<string> updateProperties) where T : class
        {
            int entityId;

            while (true)
            {
                Console.Write($"Введите ID {entityName} для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out entityId) || entityId <= 0)
                {
                    Console.WriteLine("Неверный формат ID. Пожалуйста, введите корректный ID.");
                }
                else
                {
                    // Находим запись по ID
                    var entity = context.Set<T>().Find(entityId);
                    if (entity == null)
                    {
                        Console.WriteLine($"{entityName} с указанным ID не найден. Пожалуйста, введите существующий ID.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Текущие значения {entityName} (ID = {entityId}):");

                        // Вывод текущих значений свойств
                        foreach (var property in updateProperties)
                        {
                            var propertyInfo = typeof(T).GetProperty(property);
                            if (propertyInfo != null)
                            {
                                var value = propertyInfo.GetValue(entity);
                                Console.WriteLine($"{property}: {value}");
                            }
                        }

                        Console.WriteLine();
                        Console.WriteLine($"Введите новые значения для {entityName} (ID = {entityId}):");

                        foreach (var property in updateProperties)
                        {
                            // Ищем свойство по имени
                            var propertyInfo = typeof(T).GetProperty(property);
                            if (propertyInfo == null)
                            {
                                Console.WriteLine($"Свойство {property} не найдено в {entityName}. Пропускаем.");
                                continue;
                            }

                            string newValue;

                            if (propertyInfo.PropertyType == typeof(int))
                            {
                                int newValueInt;
                                while (true)
                                {
                                    Console.Write($"{property}: ");
                                    string newValueStr = Console.ReadLine();

                                    if (int.TryParse(newValueStr, out newValueInt))
                                    {
                                        propertyInfo.SetValue(entity, newValueInt);
                                        context.SaveChanges(); // Сохраняем изменения после обновления свойства
                                        Console.WriteLine($"{entityName} с ID {entityId} успешно обновлено.");
                                        break; // Выход из цикла после успешного ввода
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Неверный формат значения для свойства {property}. Пожалуйста, введите корректное целое число.");
                                    }
                                }
                            }
                            else
                            {
                                while (true)
                                {
                                    Console.Write($"{property}: ");
                                    newValue = Console.ReadLine();

                                    if (string.IsNullOrWhiteSpace(newValue))
                                    {
                                        Console.WriteLine($"Значение для свойства {property} не может быть пустым. Пожалуйста, введите значение.");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);
                                            propertyInfo.SetValue(entity, convertedValue);
                                            context.SaveChanges(); // Сохраняем изменения после обновления свойства
                                            Console.WriteLine($"{entityName} с ID {entityId} успешно обновлено.");
                                            break; // Выход из цикла после успешного ввода
                                        }
                                        catch (InvalidCastException)
                                        {
                                            Console.WriteLine($"Неверный формат значения для свойства {property}. Пожалуйста, введите корректное значение.");
                                        }
                                    }
                                }
                            }
                        }
                        break; // Выход из цикла после успешного обновления
                    }
                }
            }
        }


        static void DeleteRecord(TransportCompanyContext context)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите сущность для удаления (введите номер):");
            Console.WriteLine("1. Марка");
            Console.WriteLine("2. Модель");
            Console.WriteLine("3. Тип");
            Console.WriteLine("4. Транспортное средство");
            Console.WriteLine("5. Транспорт_водитель");
            Console.WriteLine("6. Водитель");
            Console.WriteLine("7. Водитель_категория");
            Console.WriteLine("8. Категория");
            Console.WriteLine("0. Вернуться в предыдущее меню");
            Console.Write("Введите номер сущности (от 0 до 8): ");

            if (!int.TryParse(Console.ReadLine(), out int entityNumber))
            {
                Console.WriteLine("Неверный формат ввода. Введите число от 0 до 8.");
                return;
            }

            Console.Clear();

            switch (entityNumber)
            {
                case 1:
                    DeleteEntity<StampTransport>(context, "Марка", "stampId");
                    break;
                case 2:
                    DeleteEntity<ModelTransport>(context, "Модель", "modelId");
                    break;
                case 3:
                    DeleteEntity<TypeTransport>(context, "Тип", "typeId");
                    break;
                case 4:
                    DeleteEntity<TransportVehicle>(context, "Транспортное средство", "transportVehicleId");
                    break;
                case 5:
                    DeleteEntity<TransportVehicleDriver>(context, "Транспорт_водитель", "transportVehicleId", "driverId");
                    break;
                case 6:
                    DeleteEntity<Driver>(context, "Водитель", "driverId");
                    break;
                case 7:
                    DeleteEntity<DriverCategory>(context, "Водитель_категория", "driverId", "categoryId");
                    break;
                case 8:
                    DeleteEntity<Category>(context, "Категория", "categoryId");
                    break;
                case 0:
                    Console.WriteLine("Выход из меню удаления.");
                    return;
                default:
                    Console.WriteLine("Неверный номер сущности. Пожалуйста, введите число от 0 до 8.");
                    break;
            }
        }

        static void DeleteEntity<T>(TransportCompanyContext context, string entityName, params string[] primaryKeyNames) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var keyProperties = entityType.FindPrimaryKey().Properties;

            if (keyProperties.Count != primaryKeyNames.Length)
            {
                Console.WriteLine($"Неверное количество значений первичных ключей для {entityName}.");
                return;
            }

            var keyValues = new object[keyProperties.Count];
            for (int i = 0; i < keyProperties.Count; i++)
            {
                var keyProperty = keyProperties[i];
                Console.Write($"Введите значение {keyProperty.Name} для удаления {entityName}: ");
                var keyValue = Console.ReadLine();

                if (keyProperty.ClrType == typeof(int))
                {
                    if (!int.TryParse(keyValue, out int intValue))
                    {
                        Console.WriteLine($"Неверный формат значения первичного ключа {keyProperty.Name} для {entityName}.");
                        return;
                    }
                    keyValues[i] = intValue;
                }
                else if (keyProperty.ClrType == typeof(string))
                {
                    keyValues[i] = keyValue;
                }
                else
                {
                    Console.WriteLine($"Неподдерживаемый тип значения первичного ключа {keyProperty.Name} для {entityName}.");
                    return;
                }
            }

            // Проверяем, существует ли запись с указанными значениями первичных ключей
            var entityToDelete = context.Set<T>().Find(keyValues);

            if (entityToDelete == null)
            {
                Console.WriteLine($"{entityName} с указанными значениями первичных ключей не найдена.");
                return;
            }

            // Удаляем запись из базы данных
            context.Set<T>().Remove(entityToDelete);

            try
            {
                // Сохраняем изменения в базе данных
                context.SaveChanges();
                Console.WriteLine($"{entityName} успешно удалена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении {entityName}: {ex.Message}");
            }
        }

        static void ExecuteQuery(TransportCompanyContext context)
        {
            StringBuilder sqlQuery = new StringBuilder();

            Console.WriteLine("Введите SQL-запрос для выполнения (для выполнения запроса, введите 'go' в конце):");
            //string sqlQuery = Console.ReadLine();

            while (true)
            {
                string line = Console.ReadLine();

                if (line.ToLower() == "go")
                {
                    break; // Завершить ввод и выполнить запрос
                }

                sqlQuery.AppendLine(line);
            }

            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlQuery.ToString();
                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Результат запроса:");

                            // Получение ширины каждого столбца
                            int columnCount = result.FieldCount;
                            int[] columnWidths = new int[columnCount];

                            for (int i = 0; i < columnCount; i++)
                            {
                                columnWidths[i] = Math.Max(result.GetName(i).Length, 20); // Минимальная ширина 20 символов
                            }

                            // Вывод заголовков столбцов
                            for (int i = 0; i < columnCount; i++)
                            {
                                Console.Write($"| {result.GetName(i).PadRight(columnWidths[i])} ");
                            }
                            Console.WriteLine("|");

                            // Вывод разделителя
                            for (int i = 0; i < columnCount; i++)
                            {
                                Console.Write($"| {new string('=', columnWidths[i])} ");
                            }
                            Console.WriteLine("|");

                            // Вывод данных
                            while (result.Read())
                            {
                                for (int i = 0; i < columnCount; i++)
                                {
                                    Console.Write($"| {result[i].ToString().PadRight(columnWidths[i])} ");
                                }
                                Console.WriteLine("|");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Запрос вернул пустой результат.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }


        static void SeedData(TransportCompanyContext context)
        {
            // Заполнение таблицы StampTransport
            context.Stamps.Add(new StampTransport { title = "Toyota" });
            context.Stamps.Add(new StampTransport { title = "Ford" });
            context.Stamps.Add(new StampTransport { title = "Chevrolet" });

            context.SaveChanges();

            // Заполнение таблицы ModelTransport
            context.Models.Add(new ModelTransport { stampId = 1, title = "Camry" });
            context.Models.Add(new ModelTransport { stampId = 2, title = "F-150" });
            context.Models.Add(new ModelTransport { stampId = 3, title = "Cruze" });

            context.SaveChanges();

            // Заполнение таблицы TypeTransport
            context.Types.Add(new TypeTransport { title = "Легковой автомобиль" });
            context.Types.Add(new TypeTransport { title = "Грузовик" });
            context.Types.Add(new TypeTransport { title = "Автобус" });

            context.SaveChanges();

            // Заполнение таблицы Category
            context.Categories.Add(new Category { title = "A" });
            context.Categories.Add(new Category { title = "B" });
            context.Categories.Add(new Category { title = "C" });

            context.SaveChanges();

            // Заполнение таблицы TransportVehicle
            context.TransportVehicles.Add(new TransportVehicle
            {
                modelId = 1,
                typeId = 1,
                number = "ABC123",
                codeRegion = 21,
                dateLastInspection = new DateOnly(2023, 01, 15),
                engineNumber = "12R456T890",
                numberSeats = 5,
                maxSpeed = 140,
                releaseDate = new DateOnly(1998, 05, 06)
            });
            context.TransportVehicles.Add(new TransportVehicle
            {
                modelId = 2,
                typeId = 2,
                number = "DEF456",
                codeRegion = 136,
                dateLastInspection = new DateOnly(2023, 02, 20),
                engineNumber = "2345V78901",
                numberSeats = 2,
                maxSpeed = 120,
                releaseDate = new DateOnly(2009, 12, 05)
            });
            context.TransportVehicles.Add(new TransportVehicle
            {
                modelId = 3,
                typeId = 3,
                number = "GHI789",
                codeRegion = 45,
                dateLastInspection = new DateOnly(2023, 03, 25),
                engineNumber = "3456789012",
                numberSeats = 20,
                numberStandingPlaces = 10,
                maxSpeed = 160,
                releaseDate = new DateOnly(2023, 01, 01)
            });

            context.SaveChanges();

            // Заполнение таблицы Driver
            context.Drivers.Add(new Driver
            {
                surname = "Иванов",
                name = "Алексей",
                patronymic = "Сергеевич",
                dateOfBirth = new DateOnly(1985, 05, 15),
                phone = "+7 (123) 453-7890",
                driverLicenseNumber = "13 34 56789",
                validityPeriodRights = new DateOnly(2025, 05, 15),
                experience = new DateOnly(2022, 09, 15),
                address = "Московская область, Москва, Профсоюзная улица, 123, квартира 45"
            });
            context.Drivers.Add(new Driver
            {
                surname = "Петров",
                name = "Иван",
                patronymic = "Александрович",
                dateOfBirth = new DateOnly(1990, 02, 20),
                phone = "+7 (987) 654-3210",
                driverLicenseNumber = "98 77 54321",
                validityPeriodRights = new DateOnly(2026, 02, 20),
                experience = new DateOnly(2013, 12, 08),
                address = "Санкт-Петербург, Санкт-Петербург, Невский проспект, 56, квартира 12"
            });
            context.Drivers.Add(new Driver
            {
                surname = "Сидорова",
                name = "Мария",
                patronymic = "Петровна",
                dateOfBirth = new DateOnly(1988, 09, 10),
                phone = "+7 (487) 678-2548",
                driverLicenseNumber = "15 48 54876",
                validityPeriodRights = new DateOnly(2026, 09, 10),
                experience = new DateOnly(2020, 05, 04),
                address = "Краснодарский край, Краснодар, ул. Красная, 789, квартира 34"
            });

            context.SaveChanges();

            // Заполнение таблицы DriverCategory
            context.DriverCategories.Add(new DriverCategory
            {
                driverId = 1,
                categoryId = 1
            });
            context.DriverCategories.Add(new DriverCategory
            {
                driverId = 2,
                categoryId = 2
            });
            context.DriverCategories.Add(new DriverCategory
            {
                driverId = 3,
                categoryId = 3
            });
            context.DriverCategories.Add(new DriverCategory
            {
                driverId = 2,
                categoryId = 1
            });

            context.SaveChanges();

            // Заполнение таблицы TransportVehicleDriver
            context.TransportVehicleDrivers.Add(new TransportVehicleDriver
            {
                driverId = 1,
                transportVehicleId = 1
            });
            context.TransportVehicleDrivers.Add(new TransportVehicleDriver
            {
                driverId = 2,
                transportVehicleId = 2
            });
            context.TransportVehicleDrivers.Add(new TransportVehicleDriver
            {
                driverId = 3,
                transportVehicleId = 3
            });


            // Сохраняем изменения в базе данных
            context.SaveChanges();
        }
    }
}
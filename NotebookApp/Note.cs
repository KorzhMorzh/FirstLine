using System;

namespace NotebookApp
{
    class Note
    {
        private static int _numberOfNotes;
        private string _name;
        private string _surname;
        private string _lastname;
        private ulong _phoneNumber;
        private string _country;
        private DateTime _birthday;
        private string _organization;
        private string _post;
        private string _other;
        private int _id;

        public string Id
        {
            get => _id.ToString();
            set => _id = int.Parse(value);
        } 

        public string Surname
        {
            get => _surname;
            set
            {
                while (true)
                {
                    
                    Notebook.CheckNecessaryField(value, out bool isCorrect);
                    if (isCorrect)
                    {
                        _surname = value;
                        break;
                    }
                    value = Console.ReadLine();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                while (true)
                {
                    Notebook.CheckNecessaryField(value, out bool isCorrect);
                    if (isCorrect)
                    {
                        _name = value;
                        break;
                    }
                    value = Console.ReadLine();
                }
            }
        }

        public string Lastname
        {
            get => _lastname;
            set
            {
                Notebook.CheckUnnecessaryField(ref value);
                _lastname = value;
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber.ToString();
            set
            {
                while (true)
                {
                    bool isUniq = true;
                    bool isCorrect = ulong.TryParse(value, out ulong phoneNumber);
                    if (!isCorrect) 
                    {
                        Console.WriteLine("Номер введен некорректно. Номер должен содержать только цифры.");
                        value = Console.ReadLine();
                        continue;
                    }

                    foreach (var item in Notebook.listNotes)
                    {
                        if (ulong.Parse(item.PhoneNumber) == phoneNumber)
                        {
                            if (phoneNumber == _phoneNumber)
                            {
                                break;
                            }
                            Console.WriteLine("Такой номер уже существует");
                            isUniq = false;
                            break;
                        }
                    }

                    if (isUniq)
                    {
                        _phoneNumber = phoneNumber;
                        break;
                    }
                    value = Console.ReadLine();
                }
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                while (true)
                {
                    Notebook.CheckNecessaryField(value, out bool isCorrect);
                    if (isCorrect)
                    {
                        _country = value;
                        break;
                    }
                    value = Console.ReadLine();
                }
            }
        }

        public string Birthday
        {
            get
            {
                if (_birthday == DateTime.MinValue)
                {
                    return "не указана";
                }

                return _birthday.ToString();
            }
            set
            {
                while (true)
                {
                    if (!DateTime.TryParse(value, out _birthday))
                    {
                        if (value == "") 
                        {
                            _birthday = new DateTime(1, 1, 1);
                            break;
                        }

                        Console.WriteLine("Вы ввели неккоректную дату. " +
                                          "Попробуйте ввести в формате ДД.ММ.ГГГГ\n" +
                                          "Если желаете оставить поле пустым, нажмите enter");
                        value = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        public string Organization
        {
            get => _organization;
            set
            {
                Notebook.CheckUnnecessaryField(ref value);
                _organization = value;
            }
        }

        public string Post
        {
            get => _post;
            set
            {
                Notebook.CheckUnnecessaryField(ref value);
                _post = value;
            }
        }

        public string Other
        {
            get => _other;
            set
            {
                Notebook.CheckUnnecessaryField(ref value);
                _other = value;
            }
        }

        public Note()
        {
            Id = _numberOfNotes++.ToString();
        }

    }
}

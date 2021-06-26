# Navigation

1.  Задания
+ [Задание 1](#Task1)
+ [Задание 2](#Task2)
+ [Задание 3](#Task3)
2.  [How to choose a startup project](#StartupProj)


# <a name="Task1"></a> Задание 1
### Текст задания:

Напишите на C# WPF-приложение, подсчитывающее и выводящее количество слов и гласных букв в текстовых строках, полученных со стороннего сервера.
Приложение должно иметь следующий интерфейс: ![Image](https://github.com/duna-one/PicturesRepo/raw/master/interface_mockup_Task1.png)

Размеры окна могут произвольно изменяться; все прочие элементы при этом должны подстраиваться под новый размер.
В текстовое поле пользователь может ввести произвольное количество идентификаторов строк. Разделителем служит запятая или точка с запятой (пробелы игнорируются – как до, так и после разделителя). Идентификатор строки является целым числом и может принимать значения в диапазоне от 1 до 20 (значения «005», «05» и «5» – эквивалентны). Некорректные идентификаторы должны игнорироваться, при этом должно выводиться предупреждение, а сами такие значения выделяться. Дубликаты должны игнорироваться (каждое значение должно учитываться только один раз).
По щелчку по кнопке "Подсчитать", располагающейся рядом с текстовым полем, приложение должно получить искомые строки, произвести подсчёт и вывести результаты.
Результаты должны выводиться в таблице, которая формируется каждый раз при подсчёте. Таблица должна содержать три столбца ("Текст", "Количество слов" и "Количество гласных"), строку заголовка и строки с данными по количеству строк текста.
Если запрос к серверу не был успешным, необходимо вывести информативное (насколько это возможно) сообщение.
Приложение должно уметь работать с текстом на русском и европейских языках.

# <a name="Task2"></a> Задание 2
### Текст задания: 

Имеется несколько файлов, содержащих перевод текстовых строк с английского языка на другие языки (по одному целевому языку на файл).

Имена файлов состоят из собственно имени файла и суффикса, который идёт через дефис и соответствует коду целевого языка (ru, it, es и т.п.). Расширение всех файлов – loc.

+ Задача «А». Свести содержимое всех упомянутых файлов в один файл формата Excel, чтобы все строки были в одной таблице в удобном для восприятия виде.
+ Задача «Б». Свести содержимое всех упомянутых файлов в один файл формата Excel, чтобы все строки было там удобно редактировать. После окончания редактуры вставить изменённый текст обратно в исходные файлы.

Опишите (словами, писать код нет необходимости), как вы будете решать задачи «А» и «Б». Будьте кратки, но постарайтесь указать все существенные детали.

### Алгоритм решения задачи:

1.  Распарсить исходные файлы в удобные для работы объекты класса. Данная задача максимально тривиальная, т.к. информация в файлах представлена в JSON формате.
2.  Разобраться как создать свой EXEL файл. Это максимально просто: файл с расшерением .xlsx - это zip архив в котором хранятся все данные таблицы, включая разметку в формате XML.
3.  Создать внутреннюю структуру EXEL файла и добавить объекты класса в файл разметки листа.
4.  Запаковать все в zip архив с расширением .xlsx

# <a name="Task3"></a> Задание 3
### Текст задания:

Напишите программу на языке C#, сопоставляющую строки по методу Петренко – Больцмана.

Индекс Петренко для первой буквы в строке равен 0,5, для каждой последующей – на единицу больше. Индекс Петренко для строки равен сумме индексов составляющих её букв, дополнительно умноженной на длину строки. Все разделительные символы, включая знаки препинания, дефисы, апострофы и пробелы, при расчёте индекса не учитываются. Например, для предложения «Не выходи из комнаты, не совершай ошибку.» индекс Петренко равен 17968,5.

Имеется набор строк на русском языке и набор строк на английском языке, при этом последние дополнительно снабжены комментарием не короче одного и не длиннее пяти слов. Комментарий идёт после основной строки и отделяется от неё символом «|».

Для каждой русской строки необходимо найти все английские строки такие, что индекс Петренко для русской строки равен сумме индекса Петренко для английской строки (без учёта комментария) и индекса комментария.

**Желательно, чтобы программа выполнялась за минимально возможное время**

### Описание работы программы:

1.  Программа считывает строки из файла (порядок строк не важен, можно перемешать английские и русские)
2.  Программа сортирует строки по двум спискам.
3.  Каждый элемент списка - это экземпляр класса, при создании которого(в конструкторе) считается индекс Петренко (запускается отдельный поток, чтобы не задерживать основной)
4.  Затем для каждого элемента списка русских строк, запускается отдельный поток, который подбирает подходящие строки из списка английских (запуск потока выполняется только после завершения вычисления индекса Петренко)
5.  После завершения всех потоков, программа возвращает результат работы.

# <a name="StartupProj"></a> How to choose a startup project:
![Image](https://github.com/duna-one/PicturesRepo/raw/master/TaskChoose.gif)

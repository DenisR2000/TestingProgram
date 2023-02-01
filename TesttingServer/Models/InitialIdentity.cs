using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TesttingServer.Models
{
    public static class InitialIdentity
    {
        private static readonly string _devaloperRoleName = "Devaloper";
        private static readonly string _designerRoleName = "Designer";
        private static readonly string _usersPassword = "user050103SANDadmin$";
        public static async System.Threading.Tasks.Task Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var context = serviceProvider.GetRequiredService<TesttingDbContext>();
                
                if (!context.Tests.Any())
                {
                    Qestion qestion1 = new Qestion()
                    {
                        QestionName = "Какой тип переменной используется в коде: int a = 5;",
                    };
                    Qestion qestion2 = new Qestion()
                    {
                        QestionName = "Что делает оператор «%»",
                    };
                    Qestion qestion3 = new Qestion()
                    {
                        QestionName = "Что сделает программа выполнив следующий код: Console.WriteLine(«Hello, World!»);",
                    };
                    Qestion qestion4 = new Qestion()
                    {
                        QestionName = "Как сделать инкрементацию числа",
                    };
                    Qestion qestion5 = new Qestion()
                    {
                        QestionName = "Как сделать декрементация числа",
                    };
                    Qestion qestion_1 = new Qestion()
                    {
                        QestionName = "Что такое айдентика?",
                    };
                    Qestion qestion_2 = new Qestion()
                    {
                        QestionName = "Нужно ли тестировать дизайн/дизайн-прототип?",
                    };
                    Qestion qestion_3 = new Qestion()
                    {
                        QestionName = "RGB - это ppi, CMYK - это ...",
                    };
                    Qestion qestion_4 = new Qestion()
                    {
                        QestionName = "Графические проекты на векторной основе ...",
                    };
                    Qestion qestion_5 = new Qestion()
                    {
                        QestionName = "Первый цвет, на который реагирует глаз, если этот цвет попадает в поле зрения.",
                    };

                    var answer_1 = new Answer() { AnswerToQuestion = "графический онлайн-редактор", Qestion = qestion_1 };
                    var answer_2 = new Answer() { AnswerToQuestion = "сфера деятельности компании", Qestion = qestion_1 };
                    var answer_3 = new Answer() { AnswerToQuestion = "система визуальных решений, помогающих однозначно идентифицировать бренд", Qestion = qestion_1 };
                    var answer_4 = new Answer() { AnswerToQuestion = "рекламная продукция", Qestion = qestion_1, IsTrue = true };

                    var answer_5 = new Answer() { AnswerToQuestion = "зависит от дизайна", Qestion = qestion_2 };
                    var answer_6 = new Answer() { AnswerToQuestion = "да", Qestion = qestion_2, IsTrue = true };
                    var answer_7 = new Answer() { AnswerToQuestion = "нет", Qestion = qestion_2 };
                    var answer_8 = new Answer() { AnswerToQuestion = "наверное", Qestion = qestion_2 };

                    var answer_9 = new Answer() { AnswerToQuestion = "пиксели на дюйм или ppi", Qestion = qestion_3 };
                    var answer_10 = new Answer() { AnswerToQuestion = "разрешение экрана", Qestion = qestion_3 };
                    var answer_11 = new Answer() { AnswerToQuestion = "точки на дюйм или dpi", Qestion = qestion_3 };
                    var answer_12 = new Answer() { AnswerToQuestion = "lpi или линии на дюйм", Qestion = qestion_3, IsTrue = true };

                    var answer_13 = new Answer() { AnswerToQuestion = "создаются цифровыми фотографами", Qestion = qestion_4 };
                    var answer_14 = new Answer() { AnswerToQuestion = "создаются только в Photoshop", Qestion = qestion_4 };
                    var answer_15 = new Answer() { AnswerToQuestion = "хорошо масштабируются", Qestion = qestion_4 };
                    var answer_16 = new Answer() { AnswerToQuestion = "радуют целеустремлённых людей", Qestion = qestion_4, IsTrue = true };

                    var answer_17 = new Answer() { AnswerToQuestion = "красный", Qestion = qestion_5 };
                    var answer_18 = new Answer() { AnswerToQuestion = "зеленый", Qestion = qestion_5 };
                    var answer_19 = new Answer() { AnswerToQuestion = "голубой", Qestion = qestion_5 };
                    var answer_20 = new Answer() { AnswerToQuestion = "белый", Qestion = qestion_5, IsTrue = true };

                    var answer1 = new Answer() { AnswerToQuestion = "Знаковое 8-бит целое", Qestion = qestion1 };
                    var answer2 = new Answer() { AnswerToQuestion = "Знаковое 64-бит целое", Qestion = qestion1 };
                    var answer3 = new Answer() { AnswerToQuestion = "Знаковое 32-бит целое", Qestion = qestion1, IsTrue = true };
                    var answer4 = new Answer() { AnswerToQuestion = "1 байт*", Qestion = qestion1 };

                    var answer5 = new Answer() { AnswerToQuestion = "Возвращает процент от суммы", Qestion = qestion2 };
                    var answer6 = new Answer() { AnswerToQuestion = "Возвращает остаток от деления", Qestion = qestion2, IsTrue = true };
                    var answer7 = new Answer() { AnswerToQuestion = "Возвращает тригонометрическую функцию", Qestion = qestion2 };
                    var answer8 = new Answer() { AnswerToQuestion = "Ни чего из выше перечисленного.", Qestion = qestion2 };

                    var answer9 = new Answer() { AnswerToQuestion = "Напишет на новой строчке Hello, World!", Qestion = qestion3, IsTrue = true };
                    var answer10 = new Answer() { AnswerToQuestion = "Напишет Hello, World!", Qestion = qestion3 };
                    var answer11 = new Answer() { AnswerToQuestion = "Удалит все значения с Hello, World!", Qestion = qestion3 };
                    var answer12 = new Answer() { AnswerToQuestion = "Вырежет слово Hello, World! из всего текста", Qestion = qestion3 };

                    var answer13 = new Answer() { AnswerToQuestion = "++", Qestion = qestion4, IsTrue = true };
                    var answer14 = new Answer() { AnswerToQuestion = "—", Qestion = qestion4 };
                    var answer15 = new Answer() { AnswerToQuestion = "%%", Qestion = qestion4 };
                    var answer16 = new Answer() { AnswerToQuestion = "!=", Qestion = qestion4 };

                    var answer17 = new Answer() { AnswerToQuestion = "%%", Qestion = qestion5 };
                    var answer18 = new Answer() { AnswerToQuestion = "—", Qestion = qestion5, IsTrue = true };
                    var answer19 = new Answer() { AnswerToQuestion = "!=", Qestion = qestion5 };
                    var answer20 = new Answer() { AnswerToQuestion = "++", Qestion = qestion5 };

                    qestion1.Answers.Add(answer1);
                    qestion1.Answers.Add(answer2);
                    qestion1.Answers.Add(answer3);
                    qestion1.Answers.Add(answer4);
                    qestion2.Answers.Add(answer5);
                    qestion2.Answers.Add(answer6);
                    qestion2.Answers.Add(answer7);
                    qestion2.Answers.Add(answer8);
                    qestion3.Answers.Add(answer9);
                    qestion3.Answers.Add(answer10);
                    qestion3.Answers.Add(answer11);
                    qestion3.Answers.Add(answer12);
                    qestion4.Answers.Add(answer13);
                    qestion4.Answers.Add(answer14);
                    qestion4.Answers.Add(answer15);
                    qestion4.Answers.Add(answer16);
                    qestion5.Answers.Add(answer17);
                    qestion5.Answers.Add(answer18);
                    qestion5.Answers.Add(answer19);
                    qestion5.Answers.Add(answer20);

                    qestion_1.Answers.Add(answer_1);
                    qestion_1.Answers.Add(answer_2);
                    qestion_1.Answers.Add(answer_3);
                    qestion_1.Answers.Add(answer_4);
                    qestion_2.Answers.Add(answer_5);
                    qestion_2.Answers.Add(answer_6);
                    qestion_2.Answers.Add(answer_7);
                    qestion_2.Answers.Add(answer_8);
                    qestion_3.Answers.Add(answer_9);
                    qestion_3.Answers.Add(answer_10);
                    qestion_3.Answers.Add(answer_11);
                    qestion_3.Answers.Add(answer_12);
                    qestion_4.Answers.Add(answer_13);
                    qestion_4.Answers.Add(answer_14);
                    qestion_4.Answers.Add(answer_15);
                    qestion_4.Answers.Add(answer_16);
                    qestion_5.Answers.Add(answer_17);
                    qestion_5.Answers.Add(answer_18);
                    qestion_5.Answers.Add(answer_19);
                    qestion_5.Answers.Add(answer_20);

                    context.Answers.AddRange(answer1, answer2, answer3, answer4, 
                        answer5, answer6, answer7, answer8, 
                        answer9, answer10, answer11, answer12,
                        answer13, answer14, answer15, answer16,
                        answer17, answer18, answer19, answer20,
                        answer_1, answer_2, answer_3, answer_4,
                        answer_5, answer_6, answer_7, answer_8,
                        answer_9, answer_10, answer_11, answer_12,
                        answer_13, answer_14, answer_15, answer_16,
                        answer_17, answer_18, answer_19, answer_20
                    );

                    Test test = new Test()
                    {
                        TestTitle = "Тест с ответами на тему программирование на языке C#",
                        TestDescription = "Вопросы по программированию",
                        Type = 1
                    };
                    Test test2 = new Test()
                    {
                        TestTitle = "Тест по графическому дизайну для начинающих",
                        TestDescription = "Вопросы по графическому дизайну",
                        Type = 2
                    };

                    test.Qestions.Add(qestion1);
                    test.Qestions.Add(qestion2);
                    test.Qestions.Add(qestion3);
                    test.Qestions.Add(qestion4);
                    test.Qestions.Add(qestion5);
                    test2.Qestions.Add(qestion_1);
                    test2.Qestions.Add(qestion_2);
                    test2.Qestions.Add(qestion_3);
                    test2.Qestions.Add(qestion_4);
                    test2.Qestions.Add(qestion_5);

                    qestion1.Test = test;
                    qestion2.Test = test;
                    qestion3.Test = test;
                    qestion4.Test = test;
                    qestion5.Test = test;
                    qestion_1.Test = test2;
                    qestion_2.Test = test2;
                    qestion_3.Test = test2;
                    qestion_4.Test = test2;
                    qestion_5.Test = test2;

                    context.Qestions.AddRange(qestion1, qestion2, qestion3, qestion4, qestion5,
                        qestion_1, qestion_2, qestion_3, qestion_4, qestion_5);
                    context.Tests.AddRange(test, test2);

                    await context.SaveChangesAsync();

                    var rolesExist = await roleManager.Roles.ToListAsync();
                    if (rolesExist.Count() == 0)
                    {
                        await roleManager.CreateAsync(new IdentityRole(_devaloperRoleName));
                        await roleManager.CreateAsync(new IdentityRole(_designerRoleName));
                    }

                    User user1 = new User { Email = "denis@gmail.com", UserName = "User1Dew0777", UserRoleType = test.Type };
                    User user2 = new User { Email = "sacha@gmail.com", UserName = "User2Designer3333", UserRoleType = test2.Type };
                    IdentityResult result = null;
                    result = await userManager.CreateAsync(user1, _usersPassword);
                    var resul2 = await userManager.CreateAsync(user2, _usersPassword);
                    if (result.Succeeded && resul2.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user1, _devaloperRoleName);
                        await userManager.AddToRoleAsync(user2, _designerRoleName);
                    }
                }
            }
            catch(Exception ex) 
            { 
            }
        }
    }
}

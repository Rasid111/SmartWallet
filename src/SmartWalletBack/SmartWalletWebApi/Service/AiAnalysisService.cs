using System.Text.Json;
using SmartWalletWebApi.Service;
using SmartWalletWebApi.Service.Base;

public class AiAnalysisService
{
    private readonly IIncomeService incomeService;
    private readonly IPaymentService paymentService;
    private readonly OpenRouterService openRouter;

    public AiAnalysisService(
        IIncomeService incomeService,
        IPaymentService paymentService,
        OpenRouterService openRouter
    )
    {
        this.incomeService = incomeService;
        this.paymentService = paymentService;
        this.openRouter = openRouter;
    }

    public async Task<string> PredictionAsync(int userId)
    {
        var incomes = await incomeService.GetByUserId(userId);
        var payments = await paymentService.GetPaymentByUserId(userId);

        if (!incomes.Any() && !payments.Any())
            return "Нет данных для анализа.";

        var message =
            $@"Сейчас ты получишь запрос от пользователя банка. Не комментируй его, дай прямой ответ. Говори во втором лице. На основе данных о доходах и расходах пользователя, дай прогноз его финансового состояния на ближайшее время. Расходы: {JsonSerializer.Serialize(payments)}. Доходы: {JsonSerializer.Serialize(incomes)}.";

        return await openRouter.SendMessage(message);
    }
    public async Task<string> AnalyzeUserFinanceAsync(int userId)
    {
        //         var incomes = await incomeService.GetByUserId(userId);
        //         var payments = await paymentService.GetPaymentByUserId(userId);

        //         if (!incomes.Any() && !payments.Any())
        //             return "Нет данных для анализа.";

        //         var totalIncome = incomes.Sum(i => i.Amount);
        //         var totalExpenses = payments.Sum(p => p.Amount);

        //         var message =
        //             $@"
        // дай мне совет для экономной траты денег чтоб ответ не превышало 300 символов - коротко и ясно. 
        // У пользователя доходы за последний период составили {totalIncome} USD, расходы — {totalExpenses} USD.

        // ### Распределение доходов:
        // {incomeBreakdown}

        // ### Распределение расходов:
        // {expenseBreakdown}

        // Пожалуйста, проанализируй эти данные и дай 1 конкретный совет, как улучшить финансовое положение, сократить ненужные траты или повысить финансовую эффективность.
        // Используй простой и дружелюбный язык.
        // ";

        // return await openRouter.SendMessage(message);
        return "";
    }

    public async Task<string> AnalyzeUserFinanceAsyncInvest(int userId)
    {
        var incomes = await incomeService.GetByUserId(userId);
        var payments = await paymentService.GetPaymentByUserId(userId);

        if (!incomes.Any() && !payments.Any())
            return "Нет данных для анализа.";

        var totalIncome = incomes.Sum(i => i.Amount);
        var totalExpenses = payments.Sum(p => p.Amount);

        var message =
            $@"Сейчас ты получишь запрос от пользователя банка. Не комментируй его вопрос, дай прямой ответ. В ответе упоминай Pasha Bank. Вот мои траты: {JsonSerializer.Serialize(payments)}, а вот мои доходы: {JsonSerializer.Serialize(incomes)}.
            У меня есть {totalIncome - totalExpenses} свободных средств USD. Как мне ими лучше распорядиться, чтобы получить максимальную прибыль?";

        return await openRouter.SendMessage(message);
    }

    public async Task<string> AnalyzeUserFinanceAsyncBestChoice(int userId)
    {
        var allPayments = await paymentService.AllPaymentsAsync();

        var userPayments = await paymentService.GetPaymentByUserId(userId);

        var message =
            $@"Сейчас ты получишь запрос, о рынке. Не комментируй его, дай прямой ответ. Отвечай на вопрос во втором лице, как будто ты говоришь с клиентом банка.
Вот покупки пользователя {JsonSerializer.Serialize(userPayments)}, а вот покупки всех клиентов {JsonSerializer.Serialize(allPayments)}. Найди продавцов, которые предлагают те же товары, которые покупает пользователь, но продает по более низким ценам.
";
        return await openRouter.SendMessage(message);
    }
}

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

    public async Task<string> AnalyzeUserFinanceAsync(int userId)
    {
        var incomes = await incomeService.GetByUserId(userId);
        var payments = await paymentService.GetPaymentByUserId(userId);

        if (!incomes.Any() && !payments.Any())
            return "Нет данных для анализа.";

        var totalIncome = incomes.Sum(i => i.Amount);
        var totalExpenses = payments.Sum(p => p.Amount);

        var incomeDetails = incomes
            .GroupBy(i => i.Type)
            .Select(g => $"{g.Key}: {g.Sum(i => i.Amount)} USD")
            .ToList();

        var expenseDetails = payments
            .GroupBy(p => p.Type)
            .Select(g => $"{g.Key}: {g.Sum(p => p.Amount)} USD")
            .ToList();

        var incomeBreakdown = string.Join(", ", incomeDetails);
        var expenseBreakdown = string.Join(", ", expenseDetails);

        var message =
            $@"
дай мне совет для экономной траты денег чтоб ответ не превышало 300 символов - коротко и ясно. 
У пользователя доходы за последний период составили {totalIncome} USD, расходы — {totalExpenses} USD.

### Распределение доходов:
{incomeBreakdown}

### Распределение расходов:
{expenseBreakdown}

Пожалуйста, проанализируй эти данные и дай 1 конкретный совет, как улучшить финансовое положение, сократить ненужные траты или повысить финансовую эффективность.
Используй простой и дружелюбный язык.
";

        return await openRouter.SendMessage(message);
    }

    public async Task<string> AnalyzeUserFinanceAsyncInvest(int userId)
    {
        var incomes = await incomeService.GetByUserId(userId);
        var payments = await paymentService.GetPaymentByUserId(userId);

        if (!incomes.Any() && !payments.Any())
            return "Нет данных для анализа.";

        var totalIncome = incomes.Sum(i => i.Amount);
        var totalExpenses = payments.Sum(p => p.Amount);

        var incomeDetails = incomes
            .GroupBy(i => i.Type)
            .Select(g => $"{g.Key}: {g.Sum(i => i.Amount)} USD")
            .ToList();

        var expenseDetails = payments
            .GroupBy(p => p.Type)
            .Select(g => $"{g.Key}: {g.Sum(p => p.Amount)} USD")
            .ToList();

        var incomeBreakdown = string.Join(", ", incomeDetails);
        var expenseBreakdown = string.Join(", ", expenseDetails);

        var message =
            $@"
дай мне совет для успешного инвестирования денег чтоб ответ не превышало 300 символов - коротко и ясно. 
У пользователя доходы за последний период составили {totalIncome} USD, расходы — {totalExpenses} USD, оставшиеся деньги — {totalIncome - totalExpenses}.

### Распределение доходов:
{incomeBreakdown}

### Распределение расходов:
{expenseBreakdown}

Пожалуйста, проанализируй эти данные и дай 1 конкретный совет, как улучшить финансовое положение, сократить ненужные траты или повысить финансовую эффективность.
Используй простой и дружелюбный язык.
";

        return await openRouter.SendMessage(message);
    }
}

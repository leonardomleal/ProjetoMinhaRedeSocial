using FluentValidation;
using MinhaRedeSocial.Aplicacao.Contratos.Request;

namespace MinhaRedeSocial.Aplicacao.Validators;

public class CadastrarPostagemRequestValidator : AbstractValidator<CadastrarPostagemRequest>
{
    public CadastrarPostagemRequestValidator()
    {
        RuleFor(x => x.Texto)
            .NotNull().WithMessage("Informe um texto para a mensagem.")
            .NotEmpty().WithMessage("Informe um texto para a mensagem.");
    } 
}
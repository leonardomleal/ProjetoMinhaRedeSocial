using FluentValidation;
using MinhaRedeSocial.Aplicacao.Contratos.Request;

namespace MinhaRedeSocial.Aplicacao.Validators;

public class CadastrarUsuarioRequestValidator : AbstractValidator<CadastrarUsuarioRequest>
{
    public CadastrarUsuarioRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Informe o nome do usuário.")
            .MaximumLength(255).WithMessage("Nome deve ter no máximo 255 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Informe o e-mail do usuário.")
            .MaximumLength(255).WithMessage("E-mail deve ter no máximo 255 caracteres.");

        RuleFor(x => x.Apelido)
            .Must(apelido => !string.IsNullOrEmpty(apelido))
            .MaximumLength(50).WithMessage("Apelido deve ter no máximo 50 caracteres.");

        RuleFor(x => x.DataNascimento)
            .Must(data => data != DateTime.MinValue)
            .WithMessage("Data de nascimento deve ser informada.");

        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("Informe o CEP do usuário.")
            .Length(8).WithMessage("CEP deve possuir 8 caracteres.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Informe a senha do usuário.")
            .MaximumLength(128).WithMessage("Senha deve ter no máximo 128 caracteres.");

        RuleFor(x => x.Foto)
            .Must(foto => !string.IsNullOrEmpty(foto))
            .MaximumLength(512).WithMessage("Tamanho da imagem maior que o permitido.");
    }
}
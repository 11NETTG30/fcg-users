using FCG.Users.Application.Identidade.DTOs;
using FCG.Users.Domain.Identidade.Entities;
using FCG.Users.Domain.Identidade.Repositories;
using Moq;

namespace FCG.Users.BDDTests.Identidade.CriarUsuario
{
	public sealed class CriarUsuarioContext
	{
		public CriarUsuarioRequest? Request { get; set; }
		public Guid? UsuarioId { get; set; }
		public Exception? Excecao { get; set; }
		public Usuario? UsuarioPersistido { get; set; }
	}
}

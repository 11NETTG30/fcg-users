using FCG.Users.Application.DTOs;
using FCG.Users.Domain.Entities;
using FCG.Users.Domain.Repositories;
using Moq;

namespace FCG.Users.BDDTests.CriarUsuario
{
	public sealed class CriarUsuarioContext
	{
		public CriarUsuarioRequest? Request { get; set; }
		public Guid? UsuarioId { get; set; }
		public Exception? Excecao { get; set; }
		public Usuario? UsuarioPersistido { get; set; }
	}
}

using Common;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICategoriaService
    {
        Task<GeneralResponse<List<Categoria>>> GetAll();
        Task<GeneralResponse<Categoria>> SaveData(CategoriaDTO categoria);
        Task<GeneralResponse<Categoria>> UpdateData(CategoriaDTO categoria);
        Task<GeneralResponse<Categoria>> DeleteData(long id);
    }
}
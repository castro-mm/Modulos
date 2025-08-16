namespace Contas.Core.Interfaces;

public interface IConvertibleToDto<TDto> where TDto : IDto
{
    TDto ConvertToDto();
    void ConvertFromDto(TDto dto);
}


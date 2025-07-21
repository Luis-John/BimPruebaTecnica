using BIM.PruebaTecnica.Entities.Exceptions;

namespace BIM.PruebaTecnica.UseCases.Validations;
public class LocalidadValidations
{
    public LocalidadValidations()
    {
    }

    public bool ValidateId(int id)
    {
        if (id == default)
            throw new BadRequestException("El identificador del localidad es requerido.");

        return true;
    }


    public bool ValidateIdUsuario(string idUsuario)
    {
        if (string.IsNullOrWhiteSpace(idUsuario))
            throw new BadRequestException("El identificador del usuario es requerido.");

        return true;
    }

    public bool ValidateIdEstado(int idEstado)
    {
        if (idEstado == default)
            throw new BadRequestException("El identificador del estado es requerido.");

        if (idEstado < 0)
            throw new BadRequestException("El identificador del estado no es valido.");

        return true;
    }

    public bool ValidateMunicipio(int idMunicipio)
    {
        if (idMunicipio == default)
            throw new BadRequestException("El identificador del municipio es requerido.");

        if (idMunicipio < 0)
            throw new BadRequestException("El identificador del municipio no es valido.");

        return true;
    }

    public bool ValidateLocalidad(string localidad)
    {
        if (string.IsNullOrEmpty(localidad))
            throw new BadRequestException("El descripcion de localidad es requerida.");

        if (localidad.Length < 3)
            throw new BadRequestException("El descripcion de localidad debe tener al menos 3 caracteres.");

        return true;
    }
    public bool ValidateCodigoPostal(int codigoPostal)
    {

        if (codigoPostal == default)
            throw new BadRequestException("El codigo Postal es requerido.");

        if (codigoPostal <= 0)
            throw new BadRequestException("El codigo Postal no puede ser negativo.");

        return true;
    }

}

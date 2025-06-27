using Registeration.DTOs;

namespace Registeration.Data
{
    public class StaticRegions
    {
        public static readonly List<RegionDTO> Regions = new()
        {
            new() { Id = Guid.Parse("40b96799-8a92-49e7-7019-08d49cea1ca7"), Name = "Արագածոտն" },
            new() { Id = Guid.Parse("60d322d1-afaa-4c8d-7018-08d49cea1ca7"), Name = "Արարատ" },
            new() { Id = Guid.Parse("01ef4db0-fe28-4817-701a-08d49cea1ca7"), Name = "Արմավիր" },
            new() { Id = Guid.Parse("930b857b-2227-473f-701b-08d49cea1ca7"), Name = "Գեղարքունիք" },
            new() { Id = Guid.Parse("7bda7f57-66b9-47d7-701c-08d49cea1ca7"), Name = "Երևան" },
            new() { Id = Guid.Parse("fd80febf-3f29-4cf3-701d-08d49cea1ca7"), Name = "Լոռի" },
            new() { Id = Guid.Parse("d7e83437-45c8-423d-b4ad-08d49c589187"), Name = "Կոտայք" },
            new() { Id = Guid.Parse("67c205c6-78af-4f9e-701e-08d49cea1ca7"), Name = "Շիրակ" },
            new() { Id = Guid.Parse("1c5a43dc-8e9f-4fa8-701f-08d49cea1ca7"), Name = "Սյունիք" },
            new() { Id = Guid.Parse("85c87376-99e1-458a-7020-08d49cea1ca7"), Name = "Վայոց Ձոր" },
            new() { Id = Guid.Parse("4f6cf95d-a321-4d6d-7021-08d49cea1ca7"), Name = "Տավուշ" }
        };
    }
}

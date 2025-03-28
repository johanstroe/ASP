

using Data.Contexts;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public interface IMemberRepository : IBaseRepository<MemberEntity, Member>
{

}
public class MemberRepository(DataContext context) : IBaseRepository<MemberEntity, Member>(context), IMemberRepository
{

}


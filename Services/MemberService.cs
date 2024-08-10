using System.Linq.Expressions;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models;

public class MemberService
{
    private readonly IRepository<Member> _dbMember;

    public MemberService(IRepository<Member> dbMember)
    {
        _dbMember = dbMember;
    }

    public async Task<Member> AddMemberAsync(Member member)
    {
        await _dbMember.CreateAsync(member);
        return member;
    }
    
    public async Task<Member> DeleteMemberAsync(Member member)
    {
        await _dbMember.DeleteAsync(member);
        return member;
    }
    
    public async Task<Member> UpdateMemberAsync(Member member)
    {
        await _dbMember.UpdateAsync(member);
        return member;
    }

    public async Task<Member?> GetMember(Expression<Func<Member,bool>>? filter = null , bool tracked = true)
    {
        Member? member = await _dbMember.GetAsync(filter , tracked);
        return member;
    }
    
    public async Task<List<Member>> GetAllMembers(Expression<Func<Member,bool>>? filter = null)
    {
        List<Member> members = await _dbMember.GetAllAsync(filter);
        return members;
    }
}

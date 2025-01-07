using KiddyRanchWeb.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddyRanchWeb.BL
{
    public class BranchManager:IBranchManager
    {
        private readonly IBranchRepo _branchRepo;

        public BranchManager(IBranchRepo branchRepo)
        {
            _branchRepo = branchRepo;
        }
        public IEnumerable<BranchDTO> GetBranchs()
        {
            IEnumerable<Branch> branchsDB = _branchRepo.GetBranchs();
            return branchsDB.Select(u => new BranchDTO
            {
                branchId = u.branchId,
                branchName = u.branchName,
                locationDescription = u.locationDescription,
                locationLink = u.locationLink,
                whatsAppLink = u.whatsAppLink,
                whatsAppNum = u.whatsAppNum,
                tiktok = u.tiktok,
                facebook = u.facebook,
                instagram = u.instagram,
                logo1 = u.logo1,
                logo2 = u.logo2,
                color1 = u.color1,
                color2 = u.color2,
                color3 = u.color3,
                startedAt = u.startedAt,
                slogan= u.slogan,
                payment = u.payment,
            });
        }
        public string Add(BranchAddDTO u)
        {
            Branch branch = new Branch
            {
                branchId = Guid.NewGuid().ToString(),
                branchName = u.branchName,
                locationDescription = u.locationDescription,
                locationLink = u.locationLink,
                whatsAppLink = u.whatsAppLink,
                whatsAppNum = u.whatsAppNum,
                slogan = u.slogan,
                tiktok =u.tiktok,
                facebook = u.facebook,
                instagram = u.instagram,
                logo1 = u.logo1,
                logo2 = u.logo2,
                color1 = u.color1,
                color2 = u.color2,
                color3 = u.color3,
                startedAt = u.startedAt,
                payment = u.payment
            };
            _branchRepo.Add(branch);
            _branchRepo.SaveChanges();
            return branch.branchId;
        }

        public bool Delete(string id)
        {
            Branch? branch = _branchRepo.GetById(id);
            if (branch == null)
            {
                return false;
            }
            _branchRepo.Delete(branch);
            _branchRepo.SaveChanges();
            return true;
        }

        public BranchDTO GetBranch(string id)
        {
            Branch? u = _branchRepo.GetById(id);
            if (u is null)
            {
                return null;
            }

            return new BranchDTO
            {
                branchId = u.branchId,
                branchName = u.branchName,
                locationDescription = u.locationDescription,
                locationLink = u.locationLink,
                whatsAppLink = u.whatsAppLink,
                whatsAppNum = u.whatsAppNum,
                tiktok = u.tiktok,
                facebook = u.facebook,
                instagram = u.instagram,
                logo1 = u.logo1,
                logo2 = u.logo2,
                color1 = u.color1,
                color2 = u.color2,
                color3 = u.color3,
                startedAt = u.startedAt,
                slogan = u.slogan,
                payment = u.payment,
            };
        }
        public Branch GetBranchById(string id)
        {
            Branch? branch = _branchRepo.GetById(id) as Branch;
            if (branch == null)
            {
                return null;
            }
            return branch;
        }

        public bool Update(BranchDTO branchRequest)
        {
            if (branchRequest.branchId == null)
            {
                return false;
            }
            Branch? branch = _branchRepo.GetById(branchRequest.branchId);
            if (branch is null)
            {
                return false;
            }
            branch.branchName = branchRequest.branchName;
            branch.locationDescription = branchRequest.locationDescription;
            branch.locationLink = branchRequest.locationLink;
            branch.whatsAppLink = branchRequest.whatsAppLink;
            branch.whatsAppNum = branchRequest.whatsAppNum;
            branch.tiktok = branchRequest.tiktok;
            branch.facebook = branchRequest.facebook;
            branch.instagram = branchRequest.instagram;
            branch.logo1 = branchRequest.logo1;
            branch.logo2 = branchRequest.logo2;
            branch.color1 = branchRequest.color1;
            branch.color2 = branchRequest.color2;
            branch.color3 = branchRequest.color3;
            branch.slogan = branchRequest.slogan;
            branch.startedAt = branchRequest.startedAt;
            branch.payment = branchRequest.payment;
            _branchRepo.Update(branch);
            _branchRepo.SaveChanges();
            return true;

        }

    }
}

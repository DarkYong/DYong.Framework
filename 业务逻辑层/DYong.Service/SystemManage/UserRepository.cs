using DYong.Code;
using DYong.Data.Contract;
using DYong.Data.Repository;
using DYong.Entity.Entitys.SystemManage;
using DYong.IService.SystemManage;

namespace DYong.Service.SystemManage
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<UserEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                _ret=db.Commit();
            }
            if (!_ret.Result) throw new System.Exception(_ret.ErrorMessage);
        }
        /// <summary>
        /// 操作用户信息
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            ResultClass<int> _ret = new ResultClass<int>();
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    userLogOnEntity.F_Id = userEntity.F_Id;
                    userLogOnEntity.F_UserId = userEntity.F_Id;
                    userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }
                _ret=db.Commit();
            }
            if (!_ret.Result) throw new System.Exception(_ret.ErrorMessage);
        }
    }
}

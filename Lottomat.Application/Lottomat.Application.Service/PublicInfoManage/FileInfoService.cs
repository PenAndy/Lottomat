using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Data;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Service.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.15 10:56
    /// 描 述：文件信息
    /// </summary>
    public class FileInfoService : RepositoryFactory<FileInfoEntity>, IFileInfoService
    {
        #region 获取数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetList(Expression<Func<FileInfoEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }
        /// <summary>
        /// 所有文件（夹）列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetList(string folderId, string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate,
                                                IsShare 
                                      FROM      Base_FileFolder  where DeleteMark = 0
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate,
                                                IsShare
                                      FROM      Base_FileInfo where DeleteMark = 0
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            if (!folderId.IsEmpty())
            {
                strSql.Append(" AND FolderId = @folderId");
                parameter.Add(DbParameters.CreateDbParameter("@folderId", folderId));
            }
            else
            {
                strSql.Append(" AND FolderId = '0'");
            }
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 文档列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetDocumentList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  FileId ,
                                    FolderId ,
                                    FileName ,
                                    FileSize ,
                                    FileType ,
                                    CreateUserId ,
                                    ModifyDate,
                                    IsShare
                            FROM    Base_FileInfo
                            WHERE   DeleteMark = 0
                                    AND FileType IN ( 'log', 'txt', 'pdf', 'doc', 'docx', 'ppt', 'pptx',
                                                      'xls', 'xlsx' )
                                    AND CreateUserId = @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetImageList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  FileId ,
                                    FolderId ,
                                    FileName ,
                                    FileSize ,
                                    FileType ,
                                    CreateUserId ,
                                    ModifyDate ,
                                    IsShare
                            FROM    Base_FileInfo
                            WHERE   DeleteMark = 0
                                    AND FileType IN ( 'ico', 'gif', 'jpeg', 'jpg', 'png', 'psd' )
                                    AND CreateUserId = @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 回收站文件（夹）列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetRecycledList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileFolder  where DeleteMark = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileInfo where DeleteMark = 1
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 我的文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetMyShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileFolder  WHERE DeleteMark = 0 AND IsShare = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileInfo WHERE DeleteMark = 0 AND IsShare = 1
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 他人文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetOthersShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                CreateUserName,
                                                ShareTime AS ModifyDate
                                      FROM      Base_FileFolder  WHERE DeleteMark = 0 AND IsShare = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                CreateUserName,
                                                ShareTime AS ModifyDate
                                      FROM      Base_FileInfo WHERE DeleteMark = 0 AND IsShare = 1
                                    ) t WHERE CreateUserId != @userId");
            var parameter = new List<DbParameter>
            {
                DbParameters.CreateDbParameter("@userId", userId)
            };
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 文件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FileInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RestoreFile(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.DeleteMark = (int)DeleteMarkEnum.Delete;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 彻底删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存文件表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileInfoEntity">文件信息实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FileInfoEntity fileInfoEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                fileInfoEntity.Modify(keyValue);
                this.BaseRepository().Update(fileInfoEntity);
            }
            else
            {
                fileInfoEntity.Create();
                this.BaseRepository().Insert(fileInfoEntity);
            }
        }
        /// <summary>
        /// 共享文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        public void ShareFile(string keyValue, int IsShare)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity
            {
                FileId = keyValue,
                IsShare = IsShare,
                ShareTime = DateTimeHelper.Now
            };
            this.BaseRepository().Update(fileInfoEntity);
        }
        #endregion
    }
}

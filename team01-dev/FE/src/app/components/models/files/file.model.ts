export interface FileDtoModel {
    id : string,
    isActive: boolean,
    isDeleted: boolean,
    createdByName: string,
    createdBy: string,
    updatedByName: string,
    updatedBy: string,
    deletedByName: string,
    deletedBy: string,
    name: string,
    url: string,
    fileExt: string,
    entityType: string,
    entityId: string,
}

export interface FileSaveModel {
    EnityType : String,
    EnityId : String,
    Files: File[],
}

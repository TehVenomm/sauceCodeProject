.class public final Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
.super Ljava/lang/Object;
.source "ConversationInboxRecord.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private archivalText:Ljava/lang/String;

.field private description:Ljava/lang/String;

.field private descriptionTimeStamp:J

.field private descriptionType:I

.field private formEmail:Ljava/lang/String;

.field private formName:Ljava/lang/String;

.field private hasOlderMessages:Ljava/lang/Boolean;

.field private imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field private lastConversationsRedactionTime:Ljava/lang/Long;

.field private lastSyncTimestamp:Ljava/lang/String;

.field private persistMessageBox:Z

.field private replyText:Ljava/lang/String;

.field private userLocalId:J


# direct methods
.method public constructor <init>(J)V
    .locals 0

    .line 64
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 65
    iput-wide p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->userLocalId:J

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)V
    .locals 2

    .line 68
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 69
    iget-wide v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->userLocalId:J

    iput-wide v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->userLocalId:J

    .line 70
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formName:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formName:Ljava/lang/String;

    .line 71
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formEmail:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formEmail:Ljava/lang/String;

    .line 72
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->description:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->description:Ljava/lang/String;

    .line 73
    iget-wide v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionTimeStamp:J

    iput-wide v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionTimeStamp:J

    .line 74
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    .line 75
    iget v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionType:I

    iput v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionType:I

    .line 76
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->archivalText:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->archivalText:Ljava/lang/String;

    .line 77
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastSyncTimestamp:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastSyncTimestamp:Ljava/lang/String;

    .line 78
    iget-boolean v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->persistMessageBox:Z

    iput-boolean v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->persistMessageBox:Z

    .line 79
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->replyText:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->replyText:Ljava/lang/String;

    .line 80
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    iput-object v0, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->hasOlderMessages:Ljava/lang/Boolean;

    .line 81
    iget-object p1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastConversationsRedactionTime:Ljava/lang/Long;

    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastConversationsRedactionTime:Ljava/lang/Long;

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    .locals 18

    move-object/from16 v0, p0

    .line 145
    new-instance v17, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    iget-wide v2, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->userLocalId:J

    iget-object v4, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formName:Ljava/lang/String;

    iget-object v5, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formEmail:Ljava/lang/String;

    iget-object v6, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->description:Ljava/lang/String;

    iget-wide v7, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionTimeStamp:J

    iget-object v9, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    iget v10, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionType:I

    iget-object v11, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->archivalText:Ljava/lang/String;

    iget-object v12, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->replyText:Ljava/lang/String;

    iget-boolean v13, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->persistMessageBox:Z

    iget-object v14, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastSyncTimestamp:Ljava/lang/String;

    iget-object v15, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->hasOlderMessages:Ljava/lang/Boolean;

    iget-object v1, v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastConversationsRedactionTime:Ljava/lang/Long;

    move-object/from16 v16, v1

    move-object/from16 v1, v17

    invoke-direct/range {v1 .. v16}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;-><init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/dto/ImagePickerFile;ILjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/Boolean;Ljava/lang/Long;)V

    return-object v17
.end method

.method public setArchivalText(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 115
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->archivalText:Ljava/lang/String;

    return-object p0
.end method

.method public setDescription(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 95
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->description:Ljava/lang/String;

    return-object p0
.end method

.method public setDescriptionTimeStamp(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 100
    iput-wide p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionTimeStamp:J

    return-object p0
.end method

.method public setDescriptionType(I)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 110
    iput p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->descriptionType:I

    return-object p0
.end method

.method public setFormEmail(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 90
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formEmail:Ljava/lang/String;

    return-object p0
.end method

.method public setFormName(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 85
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->formName:Ljava/lang/String;

    return-object p0
.end method

.method public setHasOlderMessages(Z)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 135
    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->hasOlderMessages:Ljava/lang/Boolean;

    return-object p0
.end method

.method public setImageAttachmentDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 105
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    return-object p0
.end method

.method public setLastConversationsRedactionTime(Ljava/lang/Long;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 140
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastConversationsRedactionTime:Ljava/lang/Long;

    return-object p0
.end method

.method public setLastSyncTimestamp(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 125
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->lastSyncTimestamp:Ljava/lang/String;

    return-object p0
.end method

.method public setPersistMessageBox(Z)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 130
    iput-boolean p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->persistMessageBox:Z

    return-object p0
.end method

.method public setReplyText(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 0

    .line 120
    iput-object p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->replyText:Ljava/lang/String;

    return-object p0
.end method

.class public Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
.super Ljava/lang/Object;
.source "ConversationInboxRecord.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    }
.end annotation


# instance fields
.field public final archivalText:Ljava/lang/String;

.field public final description:Ljava/lang/String;

.field public final descriptionTimeStamp:J

.field public final descriptionType:I

.field public final formEmail:Ljava/lang/String;

.field public final formName:Ljava/lang/String;

.field public final hasOlderMessages:Ljava/lang/Boolean;

.field public final imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field public final lastConversationsRedactionTime:Ljava/lang/Long;

.field public final lastSyncTimestamp:Ljava/lang/String;

.field public final persistMessageBox:Z

.field public final replyText:Ljava/lang/String;

.field public final userLocalId:J


# direct methods
.method public constructor <init>(JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/dto/ImagePickerFile;ILjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/Boolean;Ljava/lang/Long;)V
    .locals 0

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 33
    iput-wide p1, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->userLocalId:J

    .line 34
    iput-object p3, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formName:Ljava/lang/String;

    .line 35
    iput-object p4, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formEmail:Ljava/lang/String;

    .line 36
    iput-object p5, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->description:Ljava/lang/String;

    .line 37
    iput-wide p6, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionTimeStamp:J

    .line 38
    iput-object p8, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;

    .line 39
    iput p9, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionType:I

    .line 40
    iput-object p10, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->archivalText:Ljava/lang/String;

    .line 41
    iput-object p11, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->replyText:Ljava/lang/String;

    .line 42
    iput-boolean p12, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->persistMessageBox:Z

    .line 43
    iput-object p13, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastSyncTimestamp:Ljava/lang/String;

    .line 44
    iput-object p14, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    .line 45
    iput-object p15, p0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastConversationsRedactionTime:Ljava/lang/Long;

    return-void
.end method

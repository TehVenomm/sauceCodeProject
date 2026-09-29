.class public abstract Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.source "AttachmentMessageDM.java"


# instance fields
.field public attachmentUrl:Ljava/lang/String;

.field public contentType:Ljava/lang/String;

.field public fileName:Ljava/lang/String;

.field public filePath:Ljava/lang/String;

.field public isSecureAttachment:Z

.field public size:I


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZLcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 9

    move-object v8, p0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    move/from16 v6, p10

    move-object/from16 v7, p12

    .line 19
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    move v0, p6

    .line 20
    iput v0, v8, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->size:I

    move-object/from16 v0, p7

    .line 21
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->contentType:Ljava/lang/String;

    move-object/from16 v0, p8

    .line 22
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->attachmentUrl:Ljava/lang/String;

    move-object/from16 v0, p9

    .line 23
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->fileName:Ljava/lang/String;

    move/from16 v0, p11

    .line 24
    iput-boolean v0, v8, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->isSecureAttachment:Z

    return-void
.end method


# virtual methods
.method public getFormattedFileSize()Ljava/lang/String;
    .locals 2

    .line 28
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->size:I

    int-to-double v0, v0

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->getFormattedFileSize(D)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getFormattedFileSize(D)Ljava/lang/String;
    .locals 6

    const-string v0, " MB"

    const-wide/high16 v1, 0x4090000000000000L    # 1024.0

    cmpg-double v3, p1, v1

    if-gez v3, :cond_0

    const-string v1, " B"

    goto :goto_0

    :cond_0
    const-wide/high16 v3, 0x4130000000000000L    # 1048576.0

    cmpg-double v5, p1, v3

    if-gez v5, :cond_1

    div-double/2addr p1, v1

    const-string v1, " KB"

    goto :goto_0

    :cond_1
    div-double/2addr p1, v3

    move-object v1, v0

    .line 47
    :goto_0
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v0, :cond_2

    .line 48
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v4, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v5, "%.1f"

    new-array v3, v3, [Ljava/lang/Object;

    invoke-static {p1, p2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    aput-object p1, v3, v2

    invoke-static {v4, v5, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    goto :goto_1

    .line 51
    :cond_2
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v4, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v5, "%.0f"

    new-array v3, v3, [Ljava/lang/Object;

    invoke-static {p1, p2}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    aput-object p1, v3, v2

    invoke-static {v4, v5, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :goto_1
    return-object p1
.end method

.method isValidUriPath(Ljava/lang/String;)Z
    .locals 1

    .line 70
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "content://"

    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 58
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 59
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    if-eqz v0, :cond_0

    .line 60
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    .line 61
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->contentType:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->contentType:Ljava/lang/String;

    .line 62
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->fileName:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->fileName:Ljava/lang/String;

    .line 63
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->attachmentUrl:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->attachmentUrl:Ljava/lang/String;

    .line 64
    iget v0, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->size:I

    iput v0, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->size:I

    .line 65
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->isSecureAttachment:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->isSecureAttachment:Z

    :cond_0
    return-void
.end method

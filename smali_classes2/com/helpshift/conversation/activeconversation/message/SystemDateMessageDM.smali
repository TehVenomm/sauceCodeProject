.class public Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;
.source "SystemDateMessageDM.java"


# instance fields
.field public isFirstMessageInList:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;JZ)V
    .locals 6

    const-string v1, ""

    .line 15
    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SYSTEM_DATE:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v2, p1

    move-wide v3, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 16
    iput-boolean p4, p0, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->isFirstMessageInList:Z

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 1

    .line 28
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    if-eqz v0, :cond_0

    .line 29
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    .line 30
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public getBodyText()Ljava/lang/String;
    .locals 4

    .line 20
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v0

    .line 21
    new-instance v1, Ljava/util/Date;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    invoke-direct {v1, v2, v3}, Ljava/util/Date;-><init>(J)V

    const-string v2, "EEEE, MMMM dd, yyyy"

    .line 22
    invoke-static {v2, v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v0

    invoke-virtual {v0, v1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

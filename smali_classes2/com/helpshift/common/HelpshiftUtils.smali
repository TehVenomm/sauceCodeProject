.class public Lcom/helpshift/common/HelpshiftUtils;
.super Ljava/lang/Object;
.source "HelpshiftUtils.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static isValidHelpshiftUser(Lcom/helpshift/HelpshiftUser;)Z
    .locals 2

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return v0

    .line 12
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 13
    invoke-virtual {p0}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/helpshift/common/StringUtils;->isEmptyWithoutTrim(Ljava/lang/String;)Z

    move-result p0

    if-nez p0, :cond_2

    :cond_1
    const/4 v0, 0x1

    :cond_2
    return v0
.end method

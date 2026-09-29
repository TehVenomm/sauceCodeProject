.class final Lcom/helpshift/CoreInternal$6;
.super Ljava/lang/Object;
.source "CoreInternal.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/CoreInternal;->login(Lcom/helpshift/HelpshiftUser;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$helpshiftUser:Lcom/helpshift/HelpshiftUser;


# direct methods
.method constructor <init>(Lcom/helpshift/HelpshiftUser;)V
    .locals 0

    .line 175
    iput-object p1, p0, Lcom/helpshift/CoreInternal$6;->val$helpshiftUser:Lcom/helpshift/HelpshiftUser;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 179
    iget-object v0, p0, Lcom/helpshift/CoreInternal$6;->val$helpshiftUser:Lcom/helpshift/HelpshiftUser;

    invoke-static {v0}, Lcom/helpshift/common/HelpshiftUtils;->isValidHelpshiftUser(Lcom/helpshift/HelpshiftUser;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "Helpshift_CoreInternal"

    .line 180
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Login state changed : name : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/CoreInternal$6;->val$helpshiftUser:Lcom/helpshift/HelpshiftUser;

    invoke-virtual {v2}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 181
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    iget-object v1, p0, Lcom/helpshift/CoreInternal$6;->val$helpshiftUser:Lcom/helpshift/HelpshiftUser;

    invoke-interface {v0, v1}, Lcom/helpshift/Core$ApiProvider;->_login(Lcom/helpshift/HelpshiftUser;)Z

    goto :goto_0

    :cond_0
    const-string v0, "Helpshift_CoreInternal"

    const-string v1, "Login called with invalid helpshift user,So calling Logout"

    .line 184
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 185
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    invoke-interface {v0}, Lcom/helpshift/Core$ApiProvider;->_logout()Z

    :goto_0
    return-void
.end method

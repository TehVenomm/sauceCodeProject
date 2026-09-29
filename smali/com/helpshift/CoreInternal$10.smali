.class final Lcom/helpshift/CoreInternal$10;
.super Ljava/lang/Object;
.source "CoreInternal.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/CoreInternal;->setTheme(I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$themeResourceId:I


# direct methods
.method constructor <init>(I)V
    .locals 0

    .line 238
    iput p1, p0, Lcom/helpshift/CoreInternal$10;->val$themeResourceId:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    const-string v0, "Helpshift_CoreInternal"

    .line 241
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Setting SDK theme : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v2, p0, Lcom/helpshift/CoreInternal$10;->val$themeResourceId:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 242
    sget-object v0, Lcom/helpshift/CoreInternal;->apiProvider:Lcom/helpshift/Core$ApiProvider;

    iget v1, p0, Lcom/helpshift/CoreInternal$10;->val$themeResourceId:I

    invoke-interface {v0, v1}, Lcom/helpshift/Core$ApiProvider;->_setTheme(I)V

    return-void
.end method

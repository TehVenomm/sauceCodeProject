.class final Lcom/helpshift/support/Support$11;
.super Ljava/lang/Object;
.source "Support.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Support;->showAlertToRateApp(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$alertToRateAppListener:Lcom/helpshift/support/AlertToRateAppListener;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V
    .locals 0

    .line 985
    iput-object p1, p0, Lcom/helpshift/support/Support$11;->val$url:Ljava/lang/String;

    iput-object p2, p0, Lcom/helpshift/support/Support$11;->val$alertToRateAppListener:Lcom/helpshift/support/AlertToRateAppListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 988
    iget-object v0, p0, Lcom/helpshift/support/Support$11;->val$url:Ljava/lang/String;

    iget-object v1, p0, Lcom/helpshift/support/Support$11;->val$alertToRateAppListener:Lcom/helpshift/support/AlertToRateAppListener;

    invoke-static {v0, v1}, Lcom/helpshift/support/SupportInternal;->showAlertToRateApp(Ljava/lang/String;Lcom/helpshift/support/AlertToRateAppListener;)V

    return-void
.end method

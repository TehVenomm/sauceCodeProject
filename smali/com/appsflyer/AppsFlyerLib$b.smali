.class final Lcom/appsflyer/AppsFlyerLib$b;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/AppsFlyerLib;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "b"
.end annotation


# instance fields
.field private ʻ:Z

.field private ʼ:Ljava/util/concurrent/ExecutorService;

.field private ʽ:Ljava/lang/String;

.field private ˊ:Ljava/lang/String;

.field private ˋ:Ljava/lang/String;

.field private ˎ:Ljava/lang/String;

.field private ˏ:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;"
        }
    .end annotation
.end field

.field private final ॱ:Landroid/content/Intent;

.field private ॱॱ:Z

.field private synthetic ᐝ:Lcom/appsflyer/AppsFlyerLib;


# direct methods
.method private constructor <init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/ref/WeakReference;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/concurrent/ExecutorService;ZLandroid/content/Intent;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "ZZ",
            "Landroid/content/Intent;",
            ")V"
        }
    .end annotation

    .line 2860
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$b;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 2861
    iput-object p2, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˏ:Ljava/lang/ref/WeakReference;

    .line 2862
    iput-object p3, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˊ:Ljava/lang/String;

    .line 2863
    iput-object p4, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˋ:Ljava/lang/String;

    .line 2864
    iput-object p5, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˎ:Ljava/lang/String;

    .line 2865
    iput-object p6, p0, Lcom/appsflyer/AppsFlyerLib$b;->ʽ:Ljava/lang/String;

    const/4 p1, 0x1

    .line 2866
    iput-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib$b;->ॱॱ:Z

    .line 2867
    iput-object p7, p0, Lcom/appsflyer/AppsFlyerLib$b;->ʼ:Ljava/util/concurrent/ExecutorService;

    .line 2868
    iput-boolean p8, p0, Lcom/appsflyer/AppsFlyerLib$b;->ʻ:Z

    .line 2869
    iput-object p9, p0, Lcom/appsflyer/AppsFlyerLib$b;->ॱ:Landroid/content/Intent;

    return-void
.end method

.method synthetic constructor <init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/ref/WeakReference;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/concurrent/ExecutorService;ZLandroid/content/Intent;B)V
    .locals 0

    .line 2841
    invoke-direct/range {p0 .. p9}, Lcom/appsflyer/AppsFlyerLib$b;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/ref/WeakReference;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/concurrent/ExecutorService;ZLandroid/content/Intent;)V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 9

    .line 2873
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$b;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˏ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/Context;

    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˊ:Ljava/lang/String;

    iget-object v3, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˋ:Ljava/lang/String;

    iget-object v4, p0, Lcom/appsflyer/AppsFlyerLib$b;->ˎ:Ljava/lang/String;

    iget-object v5, p0, Lcom/appsflyer/AppsFlyerLib$b;->ʽ:Ljava/lang/String;

    iget-boolean v6, p0, Lcom/appsflyer/AppsFlyerLib$b;->ॱॱ:Z

    iget-boolean v7, p0, Lcom/appsflyer/AppsFlyerLib$b;->ʻ:Z

    iget-object v8, p0, Lcom/appsflyer/AppsFlyerLib$b;->ॱ:Landroid/content/Intent;

    invoke-static/range {v0 .. v8}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZLandroid/content/Intent;)V

    return-void
.end method

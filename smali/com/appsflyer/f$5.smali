.class final Lcom/appsflyer/f$5;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/f;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# static fields
.field private static ˋ:Ljava/lang/String;

.field private static ˎ:Ljava/lang/String;


# instance fields
.field private synthetic ˏ:Lcom/appsflyer/f;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 3006
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method constructor <init>(Lcom/appsflyer/f;)V
    .locals 0

    .line 43
    iput-object p1, p0, Lcom/appsflyer/f$5;->ˏ:Lcom/appsflyer/f;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static ˊ(Ljava/lang/String;)V
    .locals 3

    .line 3029
    sput-object p0, Lcom/appsflyer/f$5;->ˋ:Ljava/lang/String;

    .line 3031
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v1, 0x0

    .line 3033
    :goto_0
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    if-ge v1, v2, :cond_2

    if-eqz v1, :cond_1

    .line 3034
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v2

    add-int/lit8 v2, v2, -0x1

    if-ne v1, v2, :cond_0

    goto :goto_1

    :cond_0
    const-string v2, "*"

    .line 3038
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_2

    .line 3035
    :cond_1
    :goto_1
    invoke-virtual {p0, v1}, Ljava/lang/String;->charAt(I)C

    move-result v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :goto_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 3041
    :cond_2
    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    sput-object p0, Lcom/appsflyer/f$5;->ˎ:Ljava/lang/String;

    return-void
.end method

.method static ॱ(Ljava/lang/String;)V
    .locals 2

    .line 3046
    sget-object v0, Lcom/appsflyer/f$5;->ˋ:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 3047
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "AppsFlyerKey"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/f$5;->ˊ(Ljava/lang/String;)V

    .line 3050
    :cond_0
    sget-object v0, Lcom/appsflyer/f$5;->ˋ:Ljava/lang/String;

    if-eqz v0, :cond_1

    sget-object v0, Lcom/appsflyer/f$5;->ˋ:Ljava/lang/String;

    invoke-virtual {p0, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 3051
    sget-object v0, Lcom/appsflyer/f$5;->ˋ:Ljava/lang/String;

    sget-object v1, Lcom/appsflyer/f$5;->ˎ:Ljava/lang/String;

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :cond_1
    return-void
.end method


# virtual methods
.method public final run()V
    .locals 5

    .line 46
    iget-object v0, p0, Lcom/appsflyer/f$5;->ˏ:Lcom/appsflyer/f;

    iget-object v0, v0, Lcom/appsflyer/f;->ˎ:Ljava/lang/Object;

    monitor-enter v0

    .line 47
    :try_start_0
    iget-object v1, p0, Lcom/appsflyer/f$5;->ˏ:Lcom/appsflyer/f;

    invoke-virtual {v1}, Lcom/appsflyer/f;->ॱ()V

    .line 48
    iget-object v1, p0, Lcom/appsflyer/f$5;->ˏ:Lcom/appsflyer/f;

    iget-object v1, v1, Lcom/appsflyer/f;->ˋ:Landroid/os/Handler;

    iget-object v2, p0, Lcom/appsflyer/f$5;->ˏ:Lcom/appsflyer/f;

    iget-object v2, v2, Lcom/appsflyer/f;->ˊ:Ljava/lang/Runnable;

    const-wide/32 v3, 0x1b7740

    invoke-virtual {v1, v2, v3, v4}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    .line 49
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method

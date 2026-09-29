.class Lcom/zopim/android/sdk/data/i;
.super Landroid/os/AsyncTask;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/String;",
        "Ljava/lang/Void;",
        "Lcom/zopim/android/sdk/data/h;",
        ">;"
    }
.end annotation


# static fields
.field private static final a:Ljava/lang/String; = "i"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method private a(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;
    .locals 3

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/i;->c(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/i;->b(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;

    move-result-object p1

    sget-object v1, Lcom/zopim/android/sdk/data/j;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/data/h;->ordinal()I

    move-result v2

    aget v1, v1, v2

    packed-switch v1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->getInstance()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/ConnectionPath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_1
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatFormsPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatFormsPath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_2
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatAccountPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatAccountPath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_3
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_4
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatAgentsPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatAgentsPath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_5
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->getInstance()Lcom/zopim/android/sdk/data/LivechatProfilePath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatProfilePath;->update(Ljava/lang/String;)V

    goto :goto_0

    :pswitch_6
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->update(Ljava/lang/String;)V

    :goto_0
    return-object p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private b(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;
    .locals 3

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    return-object p1

    :cond_0
    :try_start_0
    const-string v0, ";"

    invoke-virtual {p1, v0}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v0

    const-string v1, ";"

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    add-int/2addr v0, v1

    const/4 v1, 0x0

    add-int/lit8 v0, v0, -0x1

    invoke-virtual {p1, v1, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/data/h;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    sget-object v0, Lcom/zopim/android/sdk/data/i;->a:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Failed to parse the json message in order to retrieve path name. "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/IndexOutOfBoundsException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    sget-object p1, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    return-object p1
.end method

.method private c(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    if-nez p1, :cond_0

    const-string p1, ""

    return-object p1

    :cond_0
    :try_start_0
    const-string v0, ";"

    invoke-virtual {p1, v0}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v0

    const-string v1, ";"

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    add-int/2addr v0, v1

    invoke-virtual {p1, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    sget-object v0, Lcom/zopim/android/sdk/data/i;->a:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Failed to parse the json message in order to retrieve message body. "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/IndexOutOfBoundsException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string p1, ""

    return-object p1
.end method


# virtual methods
.method protected varargs a([Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;
    .locals 1

    const/4 v0, 0x0

    aget-object p1, p1, v0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/i;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;

    move-result-object p1

    return-object p1
.end method

.method protected a(Lcom/zopim/android/sdk/data/h;)V
    .locals 0

    return-void
.end method

.method protected synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    check-cast p1, [Ljava/lang/String;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/i;->a([Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;

    move-result-object p1

    return-object p1
.end method

.method protected synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Lcom/zopim/android/sdk/data/h;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/i;->a(Lcom/zopim/android/sdk/data/h;)V

    return-void
.end method

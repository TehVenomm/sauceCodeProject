.class public Lcom/helpshift/util/ConnectivityUtil;
.super Ljava/lang/Object;
.source "ConnectivityUtil.java"


# instance fields
.field private final defaultBatchSize:I

.field private final maximumBatchSize:I


# direct methods
.method public constructor <init>(II)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput p1, p0, Lcom/helpshift/util/ConnectivityUtil;->defaultBatchSize:I

    .line 13
    iput p2, p0, Lcom/helpshift/util/ConnectivityUtil;->maximumBatchSize:I

    return-void
.end method


# virtual methods
.method public getBatchSize()I
    .locals 2

    .line 18
    invoke-static {}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getInstance()Lcom/helpshift/network/connectivity/HSConnectivityManager;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/network/connectivity/HSConnectivityManager;->getConnectivityType()Lcom/helpshift/network/connectivity/HSConnectivityType;

    move-result-object v0

    .line 19
    sget-object v1, Lcom/helpshift/util/ConnectivityUtil$1;->$SwitchMap$com$helpshift$network$connectivity$HSConnectivityType:[I

    invoke-virtual {v0}, Lcom/helpshift/network/connectivity/HSConnectivityType;->ordinal()I

    move-result v0

    aget v0, v1, v0

    packed-switch v0, :pswitch_data_0

    .line 33
    iget v0, p0, Lcom/helpshift/util/ConnectivityUtil;->defaultBatchSize:I

    goto :goto_0

    .line 30
    :pswitch_0
    iget v0, p0, Lcom/helpshift/util/ConnectivityUtil;->defaultBatchSize:I

    div-int/lit8 v0, v0, 0x2

    goto :goto_0

    .line 27
    :pswitch_1
    iget v0, p0, Lcom/helpshift/util/ConnectivityUtil;->defaultBatchSize:I

    mul-int/lit8 v0, v0, 0x4

    goto :goto_0

    .line 24
    :pswitch_2
    iget v0, p0, Lcom/helpshift/util/ConnectivityUtil;->maximumBatchSize:I

    goto :goto_0

    .line 21
    :pswitch_3
    iget v0, p0, Lcom/helpshift/util/ConnectivityUtil;->defaultBatchSize:I

    :goto_0
    return v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

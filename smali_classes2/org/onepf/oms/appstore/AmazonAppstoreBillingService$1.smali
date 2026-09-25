.class synthetic Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;
.super Ljava/lang/Object;
.source "AmazonAppstoreBillingService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

.field static final synthetic $SwitchMap$com$amazon$device$iap$model$ProductType:[I

.field static final synthetic $SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

.field static final synthetic $SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

.field static final synthetic $SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 348
    invoke-static {}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->values()[Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    const/4 v0, 0x1

    :try_start_0
    sget-object v1, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    sget-object v2, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v2

    aput v0, v1, v2
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    const/4 v1, 0x2

    :try_start_1
    sget-object v2, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    sget-object v3, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->INVALID_SKU:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {v3}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v3

    aput v1, v2, v3
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    const/4 v2, 0x3

    :try_start_2
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ALREADY_PURCHASED:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v2, v3, v4
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    :try_start_3
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v4

    const/4 v5, 0x4

    aput v5, v3, v4
    :try_end_3
    .catch Ljava/lang/NoSuchFieldError; {:try_start_3 .. :try_end_3} :catch_3

    :catch_3
    :try_start_4
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->NOT_SUPPORTED:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v4

    const/4 v5, 0x5

    aput v5, v3, v4
    :try_end_4
    .catch Ljava/lang/NoSuchFieldError; {:try_start_4 .. :try_end_4} :catch_4

    .line 281
    :catch_4
    invoke-static {}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->values()[Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    :try_start_5
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_5
    .catch Ljava/lang/NoSuchFieldError; {:try_start_5 .. :try_end_5} :catch_5

    :catch_5
    :try_start_6
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v1, v3, v4
    :try_end_6
    .catch Ljava/lang/NoSuchFieldError; {:try_start_6 .. :try_end_6} :catch_6

    :catch_6
    :try_start_7
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->NOT_SUPPORTED:Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v2, v3, v4
    :try_end_7
    .catch Ljava/lang/NoSuchFieldError; {:try_start_7 .. :try_end_7} :catch_7

    .line 257
    :catch_7
    invoke-static {}, Lcom/amazon/device/iap/model/ProductType;->values()[Lcom/amazon/device/iap/model/ProductType;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductType:[I

    :try_start_8
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductType:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductType;->CONSUMABLE:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductType;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_8
    .catch Ljava/lang/NoSuchFieldError; {:try_start_8 .. :try_end_8} :catch_8

    :catch_8
    :try_start_9
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductType:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductType;->ENTITLED:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductType;->ordinal()I

    move-result v4

    aput v1, v3, v4
    :try_end_9
    .catch Ljava/lang/NoSuchFieldError; {:try_start_9 .. :try_end_9} :catch_9

    :catch_9
    :try_start_a
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductType:[I

    sget-object v4, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/ProductType;->ordinal()I

    move-result v4

    aput v2, v3, v4
    :try_end_a
    .catch Ljava/lang/NoSuchFieldError; {:try_start_a .. :try_end_a} :catch_a

    .line 216
    :catch_a
    invoke-static {}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->values()[Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

    :try_start_b
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_b
    .catch Ljava/lang/NoSuchFieldError; {:try_start_b .. :try_end_b} :catch_b

    :catch_b
    :try_start_c
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v1, v3, v4
    :try_end_c
    .catch Ljava/lang/NoSuchFieldError; {:try_start_c .. :try_end_c} :catch_c

    .line 143
    :catch_c
    invoke-static {}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->values()[Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    :try_start_d
    sget-object v3, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    sget-object v4, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_d
    .catch Ljava/lang/NoSuchFieldError; {:try_start_d .. :try_end_d} :catch_d

    :catch_d
    :try_start_e
    sget-object v0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    sget-object v3, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    invoke-virtual {v3}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->ordinal()I

    move-result v3

    aput v1, v0, v3
    :try_end_e
    .catch Ljava/lang/NoSuchFieldError; {:try_start_e .. :try_end_e} :catch_e

    :catch_e
    :try_start_f
    sget-object v0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    sget-object v1, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->NOT_SUPPORTED:Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    invoke-virtual {v1}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->ordinal()I

    move-result v1

    aput v2, v0, v1
    :try_end_f
    .catch Ljava/lang/NoSuchFieldError; {:try_start_f .. :try_end_f} :catch_f

    :catch_f
    return-void
.end method

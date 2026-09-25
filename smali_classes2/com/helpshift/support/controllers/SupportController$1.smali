.class synthetic Lcom/helpshift/support/controllers/SupportController$1;
.super Ljava/lang/Object;
.source "SupportController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/controllers/SupportController;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 199
    invoke-static {}, Lcom/helpshift/account/domainmodel/UserSetupState;->values()[Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->NON_STARTED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->FAILED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    :try_start_3
    sget-object v0, Lcom/helpshift/support/controllers/SupportController$1;->$SwitchMap$com$helpshift$account$domainmodel$UserSetupState:[I

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupState;->ordinal()I

    move-result v1

    const/4 v2, 0x4

    aput v2, v0, v1
    :try_end_3
    .catch Ljava/lang/NoSuchFieldError; {:try_start_3 .. :try_end_3} :catch_3

    :catch_3
    return-void
.end method

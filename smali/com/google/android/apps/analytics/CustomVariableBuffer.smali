.class Lcom/google/android/apps/analytics/CustomVariableBuffer;
.super Ljava/lang/Object;


# instance fields
.field private customVariables:[Lcom/google/android/apps/analytics/CustomVariable;


# direct methods
.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x5

    new-array v0, v0, [Lcom/google/android/apps/analytics/CustomVariable;

    iput-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    return-void
.end method

.method private throwOnInvalidIndex(I)V
    .locals 1

    const/4 v0, 0x1

    if-lt p1, v0, :cond_0

    const/4 v0, 0x5

    if-gt p1, v0, :cond_0

    return-void

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Index must be between 1 and 5 inclusive."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method public clearCustomVariableAt(I)V
    .locals 2

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->throwOnInvalidIndex(I)V

    iget-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    add-int/lit8 p1, p1, -0x1

    const/4 v1, 0x0

    aput-object v1, v0, p1

    return-void
.end method

.method public getCustomVariableArray()[Lcom/google/android/apps/analytics/CustomVariable;
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    invoke-virtual {v0}, [Lcom/google/android/apps/analytics/CustomVariable;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/google/android/apps/analytics/CustomVariable;

    return-object v0
.end method

.method public getCustomVariableAt(I)Lcom/google/android/apps/analytics/CustomVariable;
    .locals 1

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->throwOnInvalidIndex(I)V

    iget-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    add-int/lit8 p1, p1, -0x1

    aget-object p1, v0, p1

    return-object p1
.end method

.method public hasCustomVariables()Z
    .locals 3

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    iget-object v2, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    array-length v2, v2

    if-ge v1, v2, :cond_1

    iget-object v2, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    aget-object v2, v2, v1

    if-eqz v2, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return v0
.end method

.method public isIndexAvailable(I)Z
    .locals 2

    invoke-direct {p0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->throwOnInvalidIndex(I)V

    iget-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    const/4 v1, 0x1

    sub-int/2addr p1, v1

    aget-object p1, v0, p1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public setCustomVariable(Lcom/google/android/apps/analytics/CustomVariable;)V
    .locals 2

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v0

    invoke-direct {p0, v0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->throwOnInvalidIndex(I)V

    iget-object v0, p0, Lcom/google/android/apps/analytics/CustomVariableBuffer;->customVariables:[Lcom/google/android/apps/analytics/CustomVariable;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    aput-object p1, v0, v1

    return-void
.end method

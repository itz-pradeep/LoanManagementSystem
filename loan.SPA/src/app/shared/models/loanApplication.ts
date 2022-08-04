export interface ILoanApplication {
    id: number;
    firstName: string;
    lastName: string;
    createdDate: Date;
    propertyAddress: string;
    loanAmount: number;
    loanTenure: number;
    loanType: string;
    loanStatus: string;
    isActive: boolean;
}